namespace CacheManager.Domain.Models
{
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public CacheItem(T value)
        {
            value = Value;
        }

    }
}
