namespace AutomatedTransportEnquiry.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GoogleLoginDto
    {
        public string IdToken { get; set; }
    }


}
