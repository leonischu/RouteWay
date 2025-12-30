namespace AutomatedTransportEnquiry.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int VehicleId { get; set; }

        public int RouteId { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }

    }
}
