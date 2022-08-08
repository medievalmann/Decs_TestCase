using CacheManager.Domain.Interfaces;

namespace CacheManager.Domain.Models
{
    public class RedisCacheConnectionSetting : IConnectionSetting
    {
        public string ConnectionString { get; set; }
    }
}
