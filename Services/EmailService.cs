using SmartReplySystem.Interfaces;

namespace SmartReplySystem.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string to, string name, string body)
        {
            try
            {
                // Simulate sending an email (replace this with actual email-sending logic)
                await Task.Delay(1000); // Simulate an async task (like sending an email)

                // If email is sent successfully, return true
                return true;
            }
            catch (Exception)
            {
                // Log exception (optional) and return false if something goes wrong
                return false;
            }
        }
    }
}
        