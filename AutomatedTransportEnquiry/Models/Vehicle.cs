namespace AutomatedTransportEnquiry.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }
        public int? RouteId { get; set; }
    }
}
