namespace AutomatedTransportEnquiry.DTOs
{
    public class TransportSearchRequestDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime? TravelDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class TransportSearchResultDto
    {

        public int ScheduleId { get; set; }
        public int FareId { get; set; }

        public string RouteName { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
