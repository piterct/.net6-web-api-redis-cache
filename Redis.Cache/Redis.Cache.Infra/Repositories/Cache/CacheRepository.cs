using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;

namespace Redis.Cache.Infra.Repositories.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public CacheRepository(
            IDistributedCache distributedCache
            )
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetValue<T>(Guid id)
        {
            var key = id.ToString().ToLower();

            var result = await _distributedCache.GetStringAsync(key);
            if (result == null)
                return default;

            return JsonConvert.DeserializeObject<T?>(result);
        }

        public async Task<IEnumerable<T?>?> GetColletion<T>(string colletionKey)
        {
            var colletion = colletionKey.ToString().ToLower();

            var result = await _distributedCache.GetStringAsync(colletion);
            if (result == null)
                return default;

            return JsonConvert.DeserializeObject<IEnumerable<T?>>(result);
        }

        public async Task SetValue<T>(Guid id, T obj,
            int absoluteExpirationRelativeToNow = 300, int slidingExpiration = 300)
        {
            var key = id.ToString().ToLower();
            var newValue = JsonConvert.SerializeObject(obj);
            await _distributedCache
                .SetStringAsync(key, newValue, DistributedCacheEntryOptions(absoluteExpirationRelativeToNow, slidingExpiration));
        }

        public async Task SetColletion<T>(string collectionKey, IEnumerable<T> colletion,
            int absoluteExpirationRelativeToNow = 300, int slidingExpiration = 300)
        {
            var key = collectionKey.ToString().ToLower();
            var newColletion = JsonConvert.SerializeObject(colletion);
            await _distributedCache
                .SetStringAsync(key, newColletion, DistributedCacheEntryOptions(absoluteExpirationRelativeToNow, slidingExpiration));
        }

        public async Task RemoveAsync(Guid id)
        {
            var key = id.ToString().ToLower();
            await _distributedCache.RemoveAsync(key);
        }

        private DistributedCacheEntryOptions DistributedCacheEntryOptions(int absoluteExpirationRelativeToNow, int slidingExpiration)
        {
            return  new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpiration),
            };
        }


    }
}
