using CacheManager.Domain.Models;

namespace CacheManager.Domain.Interfaces
{
    public interface ICacheConfigurationBuilder
    {
        IDistributedCacheConfigurationBuilder WithRedisCacheProvider(RedisCacheConnectionSetting connectionSetting);
    }
}
