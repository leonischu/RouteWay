
using Microsoft.Extensions.Caching.Memory;

namespace AutomatedTransportEnquiry.Services
{
    public class MemoryCacheService : ICacheService
    {

        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
        {
            if (_cache.TryGetValue(key, out T cachedValue))
                return cachedValue;

            var result = await factory();

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            _cache.Set(key, result, options);

            return result;



        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
