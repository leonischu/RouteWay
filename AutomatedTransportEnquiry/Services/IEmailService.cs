namespace AutomatedTransportEnquiry.Services
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string to, string token);
    }
}
