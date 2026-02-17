namespace AutomatedTransportEnquiry.DTOs
{
    public class FareDto
    {
        public int FareId { get; set; }
        public string RouteName { get; set; }
        public decimal Price { get; set; }
        public int RouteId { get; set; }


    }

    public class FareCreateDto
    {
        public int RouteId  { get; set; }
        public decimal Price { get; set; }
    }

    public class FareUpdateDto
    {
        public int FareId { get; set; }
        public decimal Price { get; set; }
    }
    }
