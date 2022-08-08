namespace CacheManager.Domain.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);
        void Delete(string key);
        Task DeleteAsync(string key);
        T GetOrAdd<T>(string key, Func<T> valueFactory, TimeSpan? time);
        Task<T> GetOrAddAsync<T>(string key, Func<T> valueFactory, TimeSpan? time);
        void Add<T>(string key, T value, TimeSpan time);
        Task AddAsync<T>(string key, T value, TimeSpan time);
    }
}
