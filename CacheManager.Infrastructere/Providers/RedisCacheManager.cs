using CacheManager.Domain.Interfaces;
using CacheManager.Domain.Models;
using StackExchange.Redis;

namespace CacheManager.Infrastructere.Providers
{
    public class RedisCacheManager : BaseDistributedCacheManager
    {
        private readonly RedisCacheConnectionSetting _connectionSetting;
        private readonly ConnectionMultiplexer _connection;
        public RedisCacheManager(RedisCacheConnectionSetting connectionSetting, ICacheItemSerializer serializer, bool useBaseCacheModel) : base(serializer, useBaseCacheModel)
        {
            _connectionSetting = connectionSetting;
            _connection = ConnectionMultiplexer.Connect(_connectionSetting.ConnectionString);
        }

        private IDatabase RedisClient => _connection.GetDatabase();

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _connection.Dispose();

            _disposed = true;
        }
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected override byte[] GetInternal(string key)
        {
            var cacheItem = RedisClient.StringGet(key, CommandFlags.PreferMaster);
            return cacheItem;
        }
        protected override async Task<byte[]> GetInternalAsync(string key)
        {
            var cacheItem = await RedisClient.StringGetAsync(key, CommandFlags.PreferMaster);
            return cacheItem;
        }

        protected override void AddInternal(string key, string value, TimeSpan? time)
        {
            RedisClient.StringSet(key, value, time);
        }
        protected override async Task AddInternalAsync(string key, string value, TimeSpan? time)
        {
            await RedisClient.StringSetAsync(key, value, time);
        }

        protected override void DeleteInternal(string key)
        {
            RedisClient.KeyDelete(key);
        }
        protected override async Task DeleteInternalAsync(string key)
        {
            await RedisClient.KeyDeleteAsync(key);
        }


    }
}
