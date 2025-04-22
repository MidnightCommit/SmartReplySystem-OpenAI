using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using SmartReplySystem.Interfaces;

namespace SmartReplySystem.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public AiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"];
        }
        public async Task<string> GenerateReplyAsync(string message)
        {
            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful customer support assistant." },
                    new { role = "user", content = message }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestData);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to get a response from OpenAI.");

            var responseString = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(responseString);
            var reply = jsonDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return reply?.Trim() ?? "[No reply generated]";       
        }
    }
}