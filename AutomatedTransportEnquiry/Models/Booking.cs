namespace AutomatedTransportEnquiry.Models
{
    public class Booking
    {

        public int UserId { get; set; }
        public int BookingId { get; set; }
        public int ScheduleId { get; set; }
        public int FareId { get; set; }

        public string PassengerName { get; set; }
        public string PassengerPhone { get; set; }

        public int Seats { get; set; }
        public decimal TotalAmount { get; set; }

        public string BookingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
