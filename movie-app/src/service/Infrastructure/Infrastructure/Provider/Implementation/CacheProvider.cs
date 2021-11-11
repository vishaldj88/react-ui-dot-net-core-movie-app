using Infrastructure.Provider.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Provider.Implementation
{
    public class CacheProvider : ICacheProvider
    {
        private const int CacheSeconds = 10; // 10 Seconds

        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetFromCache<T>(string key) where T : class
        {
            _cache.TryGetValue(key, out T cachedResponse);
            return cachedResponse as T;
        }

        public void SetCache<T>(string key, T value) where T : class
        {
            SetCache(key, value, DateTimeOffset.Now.AddSeconds(CacheSeconds));
        }

        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
                             // Save data in cache.
            _cache.Set(key, value, duration);
        }

        public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
        {
            _cache.Set(key, value, options);
        }

        public void ClearCache(string key)
        {
            _cache.Remove(key);
        }

      
    }
}
