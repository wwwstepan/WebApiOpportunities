using MassTransit;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models;

namespace NetCoreApp.Controllers;

[ApiController]
[Route("rabbit")]
public class RabbitController
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Add message with user to queue
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get /rabbit/publish-user?Id=1&Name=John&Login=j001
    ///
    /// </remarks>
    /// <returns>status (int), 1 is Ok</returns>
    [HttpGet("publish-user")]
    public async Task<int> PublishUser([FromQuery] User user)
    {
        Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} Add to queue: {user}");
        await _publishEndpoint.Publish(user);
        return 1;
    }

    /// <summary>
    /// Add message with order to queue
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get /rabbit/publish-order?Id=1&UserId=2&Sum=330
    ///
    /// </remarks>
    /// <returns>status (int), 1 is Ok</returns>
    [HttpGet("publish-order")]
    public async Task<int> PublishOrder([FromQuery] Order order)
    {
        Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} Add to queue: {order}");
        await _publishEndpoint.Publish(order);
        return 1;
    }
}
