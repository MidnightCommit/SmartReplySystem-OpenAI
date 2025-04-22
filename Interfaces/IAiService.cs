namespace SmartReplySystem.Interfaces
{
    public interface IAiService
    {
        Task<string> GenerateReplyAsync(string message);
    }
}