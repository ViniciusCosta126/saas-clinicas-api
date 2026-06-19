namespace SaasClinicas.Api.Dtos.Errors;


public class ErrorResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public string TraceId { get; set; }
}