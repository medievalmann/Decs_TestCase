namespace CacheManager.Domain.Interfaces
{
    public interface ICacheItemSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string value);
    }
}
