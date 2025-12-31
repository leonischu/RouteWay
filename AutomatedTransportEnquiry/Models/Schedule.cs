namespace AutomatedTransportEnquiry.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }

        public int RouteId { get; set; }
        public DateTime? TravelDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public decimal Price { get; set; }

    }
}
