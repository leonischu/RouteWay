namespace AutomatedTransportEnquiry.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }

        public string RouteName { get; set; }
        public string VehicleName { get; set; }

        public TimeSpan DepartureTime { get; set; }
        public decimal TotalAmount { get; set; }

        public string BookingStatus { get; set; }
        public int ScheduleId { get; set; }
        public int FareId { get; set; }

        public string PassengerName { get; set; }
        public string PassengerPhone { get; set; }

        public int Seats { get; set; }
        public string CancellationReason { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
    public class BookingCreateDto
    {
        public int ScheduleId { get; set; }
        public int FareId { get; set; }

        public string PassengerName { get; set; }
        public string PassengerPhone { get; set; }

        public int Seats { get; set; }
    }



}
