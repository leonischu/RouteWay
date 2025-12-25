namespace AutomatedTransportEnquiry.DTOs
{
    public class VehicleRouteUpdateDto
    {
        public int RouteId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        public int Distance { get; set; }


    }
}
