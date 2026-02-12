namespace AutomatedTransportEnquiry.DTOs
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        
        public string RouteName { get; set; }
        public DateTime? TravelDate { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
    }



    public class ScheduleCreateDto
    {
        public int VehicleId { get; set; }
        public int RouteId { get; set; }
        public DateTime? TravelDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
        //public decimal Price { get; set; }
    }




}
