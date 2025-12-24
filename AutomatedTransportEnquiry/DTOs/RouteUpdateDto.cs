namespace AutomatedTransportEnquiry.DTOs
{
    public class RouteUpdateDto
    {
        public int RouteId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        public int Distance { get; set; }


    }
}
