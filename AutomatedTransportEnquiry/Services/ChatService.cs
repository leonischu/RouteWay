using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AutomatedTransportEnquiry.Services
{
    public class ChatService : IChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly IBookingRepository _bookingRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public ChatService(
            IHttpClientFactory httpClientFactory,
            IConfiguration config,
            IBookingRepository bookingRepository,
            IScheduleRepository scheduleRepository)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _bookingRepository = bookingRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<APIResponse> GetReplyAsync(ChatRequestDto dto, int userId)
        {
            var response = new APIResponse();
            try
            {
                // Validate message
                if (string.IsNullOrWhiteSpace(dto.Message))
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Errors.Add("Message cannot be empty.");
                    return response;
                }

                var dbContext = await BuildDbContext(dto.Message, userId);

                var systemPrompt = $@"You are a friendly transport booking assistant for AutomatedTransportEnquiry, a bus booking platform in Nepal.
You help users with:
1. Searching available routes and schedules
2. Checking their booking status and history
3. FAQs about booking and cancellation
Rules:

-If user or admin is not loggedIn display please login first
- Be concise and friendly
- Only answer transport-related questions
- Reply by reading available data not data from outside and if data is not available say The data you searched does not exist.
- Currency is NPR (Nepali Rupees)
- If asked something unrelated, politely redirect back to transport topics
{dbContext}";

                var messages = new List<object>
                {
                    new { role = "system", content = systemPrompt }
                };

                // ✅ Only add history entries with valid roles
                var validRoles = new[] { "user", "assistant" };
                foreach (var h in dto.History.TakeLast(6))
                {
                    if (!string.IsNullOrWhiteSpace(h.Role)
                        && !string.IsNullOrWhiteSpace(h.Content)
                        && validRoles.Contains(h.Role.ToLower().Trim()))
                    {
                        messages.Add(new
                        {
                            role = h.Role.ToLower().Trim(),
                            content = h.Content
                        });
                    }
                }

                // ✅ Add current user message
                messages.Add(new { role = "user", content = dto.Message.Trim() });

                var reply = await CallGroq(messages);

                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = new { reply };
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        private async Task<string> BuildDbContext(string message, int userId)
        {
            var context = new StringBuilder();
            var msg = message.ToLower();

            try
            {
                if (msg.Contains("route") || msg.Contains("schedule") || msg.Contains("bus") ||
                    msg.Contains("seat") || msg.Contains("available") || msg.Contains("travel") ||
                    msg.Contains("from") || msg.Contains("when") || msg.Contains("departure"))
                {
                    var schedules = await _scheduleRepository.GetUpcomingAsync();
                    if (schedules.Any())
                    {
                        context.AppendLine("AVAILABLE UPCOMING SCHEDULES:");
                        foreach (var s in schedules.Take(10))
                            context.AppendLine(
                                $"- ScheduleId:{s.ScheduleId} | RouteId:{s.RouteId} | " +
                                $"VehicleId:{s.VehicleId} | Departure:{s.DepartureTime} | " +
                                $"Arrival:{s.ArrivalTime} | Date:{s.TravelDate:MMM dd yyyy} | " +
                                $"Seats Available:{s.AvailableSeats}");
                    }
                    else
                    {
                        context.AppendLine("AVAILABLE SCHEDULES: No upcoming schedules found.");
                    }
                }

                if (msg.Contains("booking") || msg.Contains("ticket") ||
                    msg.Contains("cancel") || msg.Contains("status") ||
                    msg.Contains("my book") || msg.Contains("reservation"))
                {
                    var bookings = await _bookingRepository.GetByUserIdAsync(userId);
                    if (bookings.Any())
                    {
                        context.AppendLine("THIS USER'S BOOKINGS:");
                        foreach (var b in bookings.TakeLast(5))
                            context.AppendLine(
                                $"- BookingId:{b.BookingId} | Status:{b.BookingStatus} | " +
                                $"Seats:{b.Seats} | Amount:NPR {b.TotalAmount} | " +
                                $"Date:{b.CreatedAt:MMM dd yyyy}");
                    }
                    else
                    {
                        context.AppendLine("THIS USER'S BOOKINGS: No bookings found.");
                    }
                }
            }
            catch
            {
                // DB errors should not crash the chat
            }

            return context.Length > 0
                ? $"--- LIVE DATA FROM SYSTEM ---\n{context}\n--- END ---"
                : string.Empty;
        }

        private async Task<string> CallGroq(List<object> messages)
        {
            var apiKey = _config["Groq:ApiKey"];
            var model = _config["Groq:Model"];

            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("Groq API key is missing. Add it to appsettings.json under Groq:ApiKey");

            if (string.IsNullOrEmpty(model))
                throw new Exception("Groq model is missing. Add it to appsettings.json under Groq:Model");

            var client = _httpClientFactory.CreateClient("Groq");

            var body = new
            {
                model,
                messages,
                max_tokens = 500,
                temperature = 0.7
            };

            var content = new StringContent(
                JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync("/openai/v1/chat/completions", content);
            var json = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Groq error: {json}");

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "Sorry, I could not generate a response.";
        }
    }
}