using CacheManager.Domain.Enums;
using CacheManager.Domain.Interfaces;
using CacheManager.Domain.Models;
using CacheManager.Infrastructere.Providers;
using CacheManager.Infrastructere.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Infrastructere
{
    public class CacheConfigurationBuilder : ICacheConfigurationBuilder, IDistributedCacheConfigurationBuilder
    {
        private ICacheItemSerializer _itemSerializer;
        private CacheProviderType _providerType;
        private RedisCacheConnectionSetting _connectionSetting;
        private bool _useBaseCacheModel;

        public CacheConfigurationBuilder()
        {
            _useBaseCacheModel = true;
        }


        public IDistributedCacheManager Build()
        {
            switch (_providerType)
            {
                case CacheProviderType.Redis:
                    return new RedisCacheManager(_connectionSetting, _itemSerializer, _useBaseCacheModel);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IDistributedCacheConfigurationBuilder NotUseBaseCacheModel()
        {
            _useBaseCacheModel = false;
            return this;
        }

        public IDistributedCacheConfigurationBuilder WithRedisCacheProvider(RedisCacheConnectionSetting connectionSetting)
        {
            _providerType = CacheProviderType.Redis;
            _connectionSetting = connectionSetting;
            return this;
        }

        public IDistributedCacheConfigurationBuilder WtihJsonCacheItemSerializer()
        {
            _itemSerializer = new JsonCacheItemSerializer();
            return this;
        }
    }
}
