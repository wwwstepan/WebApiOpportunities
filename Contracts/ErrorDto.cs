using System.Text.Json;

namespace WebApiOpportunities.Contracts;

public class ErrorDto
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public override string ToString() => JsonSerializer.Serialize(this);
}
