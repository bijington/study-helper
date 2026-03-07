using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace StudyHelper.Chat;

public class ChatResponse
{
    private ChatResponse(string text, bool isSuccess)
    {
        Text = text;
        IsSuccess = isSuccess;
    }

    public static ChatResponse Ok(string text) => new ChatResponse(text, true);
    
    public static ChatResponse Error(string text) => new ChatResponse(text, false);
    
    public string Text { get; set; }
    public bool IsSuccess { get; set; }
}

public class ChatService
{
    private readonly HttpClient _httpClient;

    public ChatService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        var apiKey = configuration["OpenAI:ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }
    
    public async Task<ChatResponse> SendMessage(string prompt)
    {
        try
        {
            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://api.openai.com/v1/chat/completions",
                requestBody);

            if (response.IsSuccessStatusCode) 
            { 
                var jsonResponse = await response.Content.ReadAsStringAsync(); 
                using var doc = JsonDocument.Parse(jsonResponse);
                return ChatResponse.Ok(doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString() ?? "No response from the API.");
            }

            return ChatResponse.Error($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
        catch (Exception exception)
        {
            return ChatResponse.Error($"Error: {exception.Message}");
        }
    }
}