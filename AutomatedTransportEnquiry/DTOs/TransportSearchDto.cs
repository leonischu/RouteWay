namespace AutomatedTransportEnquiry.DTOs
{
    public class TransportSearchDto
    {
        public int ScheduleId { get; set; }
        public string VehicleName { get; set; }
        public string RouteName { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
