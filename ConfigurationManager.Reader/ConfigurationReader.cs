using CacheManager.Domain.Interfaces;
using CacheManager.Infrastructere;
using ConfigurationManager.Data;
using ConfigurationManager.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationManager.Reader
{
    public class ConfigurationReader : IConfigurationReader
    {
        private static Dictionary<string, string> _configurationValues;
        private static string _applicationName;
        private static TimeSpan _refreshTimeInterval;
        private static IDistributedCacheManager _cacheManager;
        private static string _configurationDbConnectionString;
        public ConfigurationReader(string applicationName, string redisConnectionString, TimeSpan refreshTimeInterval, string configurationDbConnectionString)
        {
            _applicationName = applicationName;
            _refreshTimeInterval = refreshTimeInterval;
            _configurationValues = new Dictionary<string, string>();

            CacheConfigurationBuilder builder = new CacheConfigurationBuilder();
            builder.WithRedisCacheProvider(new CacheManager.Domain.Models.RedisCacheConnectionSetting { ConnectionString = redisConnectionString }).WtihJsonCacheItemSerializer();

            _cacheManager = builder.Build();
            _configurationDbConnectionString = configurationDbConnectionString;
        }


        public T GetValue<T>(string key)
        {
            try
            {
                var value = _cacheManager.GetOrAdd<T>(GetKeyWithApplicationName(key), () =>
                {
                    var unitOfWork = new UnitOfWork(new ConfigurationManagerDbContext(_configurationDbConnectionString));

                    ConfigurationService configurationService = new ConfigurationService(unitOfWork);
                    var configuration = configurationService.GetByApplicationIdAndName(_applicationName, key);
                    if (configuration != null)
                        return (T)Convert.ChangeType(configuration.Value, typeof(T));
                    return default;
                }, _refreshTimeInterval);

                if (_configurationValues.Any(x => x.Key == key))
                    _configurationValues[key] = JsonSerializer.Serialize(value);
                else _configurationValues.Add(key, JsonSerializer.Serialize(value));

                return value;
            }
            catch (Exception e)
            {
                if (_configurationValues.Any(x => x.Key == key))
                    return JsonSerializer.Deserialize<T>(_configurationValues[key]);
                throw;
            }
        }

        private string GetKeyWithApplicationName(string key)
        {
            return key + "-" + _applicationName;
        }
    }
}
