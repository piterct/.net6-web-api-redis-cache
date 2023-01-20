using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;

namespace Redis.Cache.Infra.Repositories.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public CacheRepository(IDistributedCache distributedCache)
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
            var result = await _distributedCache.GetStringAsync(colletionKey);
            if (result == null)
                return default;

            return JsonConvert.DeserializeObject<IEnumerable<T?>>(result);
        }

        public async Task SetValue<T>(Guid id, T obj)
        {
            var key = id.ToString().ToLower();
            var newValue = JsonConvert.SerializeObject(obj);
            await _distributedCache.SetStringAsync(key, newValue);
        }

        public async Task SetColletion<T>(string collectionKey, IEnumerable<T> colletion)
        {
            var key = collectionKey.ToString().ToLower();
            var newColletion = JsonConvert.SerializeObject(colletion);
            await _distributedCache.SetStringAsync(key, newColletion);
        }


    }
}
