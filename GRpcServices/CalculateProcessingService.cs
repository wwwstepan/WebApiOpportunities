using Grpc.Core;

namespace WebApiOpportunities.GRpcServices;

public class CalculateProcessingService : CalculateProcessing.CalculateProcessingBase
{
    private readonly ILogger<CalculateProcessingService> _logger;

    public CalculateProcessingService(ILogger<CalculateProcessingService> logger)
    {
        _logger = logger;
    }

    public override Task<CalculateProcessingReply> Calculate(CalculateProcessingRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CalculateProcessingReply
        {
            Message = OkWithTime(),
            Result = Calculate(request)
        });
    }

    public override Task<StringMessage> Ping(Empty _, ServerCallContext context)
    {
        return Task.FromResult(new StringMessage
        {
            Message = OkWithTime()
        });
    }

    private static string OkWithTime() => $"OK {DateTime.Now:dd.MM.yyyy HH:mm:ss.fff}";

    private static int Calculate(CalculateProcessingRequest request) => request.Operation switch {
        "+" => request.Argument1 + request.Argument2,
        "-" => request.Argument1 - request.Argument2,
        "*" or "x" => request.Argument1 * request.Argument2,
        "/" => request.Argument1 / request.Argument2,
        _ => 0
    };
}
