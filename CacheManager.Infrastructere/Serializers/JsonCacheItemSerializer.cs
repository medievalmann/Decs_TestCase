using CacheManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CacheManager.Infrastructere.Serializers
{
    public class JsonCacheItemSerializer : ICacheItemSerializer
    {
        public T Deserialize<T>(string value)
        {
            if (value != null)
                return JsonSerializer.Deserialize<T>(value);
            return default;
        }

        public string Serialize<T>(T value)
        {
            if (value != null)
                return JsonSerializer.Serialize(value);

            return null;
        }
    }
}
