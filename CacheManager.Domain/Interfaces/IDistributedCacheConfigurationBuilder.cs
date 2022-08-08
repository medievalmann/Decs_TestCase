namespace CacheManager.Domain.Interfaces
{
    public interface IDistributedCacheConfigurationBuilder
    {
        IDistributedCacheConfigurationBuilder WtihJsonCacheItemSerializer();
        IDistributedCacheConfigurationBuilder NotUseBaseCacheModel();
        IDistributedCacheManager Build();
    }
}
