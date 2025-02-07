using MassTransit;
using NetCoreApp.Models;

namespace NetCoreApp.Bus;

public class ChangeOrderConsumer : IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} Received: {context.Message}");
        return Task.CompletedTask;
    }
}
