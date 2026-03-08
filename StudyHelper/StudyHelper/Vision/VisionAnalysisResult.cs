namespace StudyHelper.Vision;

/// <summary>
/// Result of a vision analysis request.
/// </summary>
public class VisionAnalysisResult
{
    /// <summary>
    /// Whether the analysis was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The AI-generated response message to show the user.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Error message if the analysis failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static VisionAnalysisResult Ok(string message)
    {
        return new VisionAnalysisResult
        {
            Success = true,
            Message = message
        };
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static VisionAnalysisResult Error(string errorMessage) => new()
    {
        Success = false,
        ErrorMessage = errorMessage
    };
}