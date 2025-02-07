using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Services;

namespace NetCoreApp.Controllers;

[ApiController]
[Route("redis")]
public class RedisController(RedisService redisService)
{
    [HttpGet("set")]
    public string Set(string key, string value)
    {
        redisService.SetValue(key, value);
        return "Ok";
    }

    [HttpGet("get")]
    public string Get(string key)
    {
        var value = redisService.GetValue(key);
        return value;
    }
}
