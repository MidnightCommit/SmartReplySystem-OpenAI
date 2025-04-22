namespace SmartReplySystem.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string name, string body); // ✅
    }
}