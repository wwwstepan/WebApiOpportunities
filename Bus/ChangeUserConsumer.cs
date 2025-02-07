using MassTransit;
using NetCoreApp.Models;

namespace NetCoreApp.Bus;

public class ChangeUserConsumer : IConsumer<User>
{
    public Task Consume(ConsumeContext<User> context)
    {
        Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} Received: {context.Message}");
        return Task.CompletedTask;
    }
}
