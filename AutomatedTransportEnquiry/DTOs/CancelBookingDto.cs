namespace AutomatedTransportEnquiry.DTOs
{

    public class CancelBookingDto
    {
        public int BookingId { get; set; }
        public string? CancellationReason { get; set; }
    }

    public class CancelBookingResponseDto
    {
        public int BookingId { get; set; }
        public string BookingStatus { get; set; }
        public decimal RefundAmount { get; set; }
        public string Message { get; set; }
    }
}
