namespace AutomatedTransportEnquiry.DTOs
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }

    }

    public class VehicleCreateDto
    {
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }
    }

    public class VehicleUpdateDto
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }
    }



}
