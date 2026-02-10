using System.ComponentModel.DataAnnotations;

namespace AutomatedTransportEnquiry.DTOs
{
    public class VehicleRouteDto
    {

     
        public int RouteId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Distance { get; set; }
    }
}
