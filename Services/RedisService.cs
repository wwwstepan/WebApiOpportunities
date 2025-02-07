using StackExchange.Redis;
using System.Text.Json;

namespace NetCoreApp.Services;

public class RedisService
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisService> _logger;

    public RedisService(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisService> logger)
    {
        _db = connectionMultiplexer.GetDatabase();
        _logger = logger;
    }

    public void SetValue(string key, string value)
    {
        try
        {
            _logger.LogInformation($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} Redis.Set {key} {value}");
            _db.StringSet(key, value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error setting value {value} in Redis");
        }
    }

    public string GetValue(string key)
    {
        try
        {
            _logger.LogInformation($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} Redis.Get {key}");
            var value = _db.StringGet(key).ToString();
            return value ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting value {key} from Redis");
            return string.Empty;
        }
    }
    
    public void SetHandbookCache(Dictionary<Guid, string> handbookCache)
    {
        try
        {
            var json = JsonSerializer.Serialize(handbookCache);
            _db.StringSet("HandbookCache", json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting HandbookCache in Redis");
        }
    }

    public Dictionary<Guid, string> GetHandbookCache()
    {
        try
        {
            var json = _db.StringGet("HandbookCache");
            if (json.HasValue)
            {
                return JsonSerializer.Deserialize<Dictionary<Guid, string>>(json);
            }
            else
            {
                return new Dictionary<Guid, string>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting HandbookCache from Redis");
            return [];
        }
    }
}
