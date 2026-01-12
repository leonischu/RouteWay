using System.Net;
using System.Net.Mail;

namespace AutomatedTransportEnquiry.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmail(string to, string token)
        {
            var baseUrl = _configuration["EmailSettings:BaseUrl"];
            var verificationLink = $"{baseUrl}/api/auth/verify-email?token={token}";

            Console.WriteLine("\n========================================");
            Console.WriteLine("📧 EMAIL VERIFICATION");
            Console.WriteLine("========================================");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Token: {token}");
            Console.WriteLine($"\n🔗 VERIFICATION LINK:");
            Console.WriteLine(verificationLink);
            Console.WriteLine("========================================\n");

            await Task.CompletedTask;
        }
    }
}
