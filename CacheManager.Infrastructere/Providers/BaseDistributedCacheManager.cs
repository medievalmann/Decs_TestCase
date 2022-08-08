using CacheManager.Domain.Interfaces;
using CacheManager.Domain.Models;
using System.Text;

namespace CacheManager.Infrastructere.Providers
{
    public abstract class BaseDistributedCacheManager : IDistributedCacheManager
    {
        public abstract void Dispose();
        protected abstract byte[] GetInternal(string key);
        protected abstract Task<byte[]> GetInternalAsync(string key);
        protected abstract void DeleteInternal(string key);
        protected abstract Task DeleteInternalAsync(string key);
        protected abstract void AddInternal(string key, string value, TimeSpan? time);
        protected abstract Task AddInternalAsync(string key, string value, TimeSpan? time);

        private readonly ICacheItemSerializer _serializer;
        private readonly bool _useBaseCacheModel;

        public BaseDistributedCacheManager(ICacheItemSerializer serializer, bool useBaseCacheModel)
        {
            _serializer = serializer;
            _useBaseCacheModel = useBaseCacheModel;
        }

        #region Public Methods
        public T Get<T>(string key)
        {
            NotNullOrWhiteSpace(key);

            var cacheValue = GetInternal(key);

            return Desrialize<T>(cacheValue);
        }
        public async Task<T> GetAsync<T>(string key)
        {
            NotNullOrWhiteSpace(key);

            var cacheValue = await GetInternalAsync(key);

            return Desrialize<T>(cacheValue);
        }

        public void Delete(string key)
        {
            NotNullOrWhiteSpace(key);

            DeleteInternal(key);
        }
        public async Task DeleteAsync(string key)
        {
            NotNullOrWhiteSpace(key);

            await DeleteInternalAsync(key);
        }

        public T GetOrAdd<T>(string key, Func<T> valueFactory, TimeSpan? time)
        {
            NotNullOrWhiteSpace(key);

            var cacheItem = GetInternal(key);
            if (cacheItem == null)
            {
                var value = valueFactory.Invoke();
                if (value != null)
                {
                    var serializeCacheItem = Serialize(value);
                    AddInternal(key, serializeCacheItem, time);
                    return value;
                }
            }

            return Desrialize<T>(cacheItem);
        }
        public async Task<T> GetOrAddAsync<T>(string key, Func<T> valueFactory, TimeSpan? time)
        {
            NotNullOrWhiteSpace(key);

            var cacheItem = await GetInternalAsync(key);
            if (cacheItem == null)
            {
                var value = valueFactory.Invoke();
                var serializeCacheItem = Serialize(value);

                await AddInternalAsync(key, serializeCacheItem, time);

                return value;
            }

            return Desrialize<T>(cacheItem);
        }

        public void Add<T>(string key, T value, TimeSpan time)
        {
            NotNullOrWhiteSpace(key);

            var serializedValue = Serialize(value);
            AddInternal(key, serializedValue, time);
        }
        public async Task AddAsync<T>(string key, T value, TimeSpan time)
        {
            NotNullOrWhiteSpace(key);

            var serializedValue = Serialize(value);
            await AddInternalAsync(key, serializedValue, time);
        }
        #endregion

        #region PrivateMethods

        private void NotNullOrWhiteSpace(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Value must not be empty", nameof(key));
        }
        private T Desrialize<T>(byte[] cacheValue)
        {
            T result;
            if (_useBaseCacheModel)
            {
                var cacheItem = _serializer.Deserialize<CacheItem<T>>(Encoding.Default.GetString(cacheValue));
                result = cacheItem.Value;
            }
            else
                result = _serializer.Deserialize<T>(Encoding.Default.GetString(cacheValue));

            return result;
        }

        private string Serialize<T>(T value)
        {
            string result;
            if (_useBaseCacheModel)
                result = _serializer.Serialize(new CacheItem<T>(value));
            else
                result = _serializer.Serialize(value);


            return result;
        }
        #endregion

    }
}
