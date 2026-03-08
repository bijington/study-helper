using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StudyHelper.Vision;

/// <summary>
/// Service for analyzing images using Azure OpenAI vision capabilities.
/// </summary>
public class VisionService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<VisionService> _logger;
    private IChatClient? _visionClient;

    private const int TimeoutSeconds = 30;

    private const string SystemPrompt = """
        Please pay particular attention to the user prompt and provide accurate responses.
        
        Rules:
        - Be friendly and helpful in your response
        - If you cannot clearly see the subject or the image is unclear, say so
        - Keep responses concise (1-2 sentences)
        """;

    public VisionService(IConfiguration configuration, ILogger<VisionService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task<bool> IsAvailableAsync()
    {
        var endpoint = _configuration["AzureOpenAI:Endpoint"];
        var apiKey = _configuration["AzureOpenAI:ApiKey"];
        var deploymentName = _configuration["AzureOpenAI:DeploymentName"];
        var isAvailable = !string.IsNullOrWhiteSpace(endpoint) && !string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(deploymentName);
        return Task.FromResult(isAvailable);
    }

    public async Task<VisionAnalysisResult> AnalyzeImageAsync(
        Stream imageStream,
        string userQuestion,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var client = GetOrCreateVisionClient();
            if (client == null)
            {
                return VisionAnalysisResult.Error("Vision service is not configured.");
            }

            // Read image to bytes and convert to base64
            using var memoryStream = new MemoryStream();
            await imageStream.CopyToAsync(memoryStream, cancellationToken);
            var imageBytes = memoryStream.ToArray();

            _logger.LogDebug("Analyzing image ({Size} bytes) with question: {Question}", 
                imageBytes.Length, userQuestion);

            // Build the multimodal message with image
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, SystemPrompt),
                new(ChatRole.User, [
                    new TextContent(userQuestion),
                    new DataContent(imageBytes, "image/jpeg")
                ])
            };

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(TimeoutSeconds));

            var response = await client.GetResponseAsync(messages, cancellationToken: cts.Token);
            var responseText = response.Text ?? "I couldn't analyze the image.";

            _logger.LogInformation("Vision analysis complete: {Response}", responseText);

            return VisionAnalysisResult.Ok(responseText);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Vision analysis timed out");
            return VisionAnalysisResult.Error("Analysis timed out. Please try again.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error analyzing image");
            return VisionAnalysisResult.Error($"Failed to analyze image: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets or creates the Azure OpenAI vision client.
    /// </summary>
    private IChatClient? GetOrCreateVisionClient()
    {
        if (_visionClient != null)
        {
            return _visionClient;
        }

        var endpoint = _configuration["AzureOpenAI:Endpoint"];
        var apiKey = _configuration["AzureOpenAI:ApiKey"];
        var deploymentName = _configuration["AzureOpenAI:DeploymentName"];

        if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(deploymentName))
        {
            return null;
        }

        try
        {
            var azureClient = new AzureOpenAIClient(
                new Uri(endpoint),
                new ApiKeyCredential(apiKey));
            _visionClient = azureClient.GetChatClient(deploymentName).AsIChatClient();
            return _visionClient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Azure OpenAI vision client");
            return null;
        }
    }
}
