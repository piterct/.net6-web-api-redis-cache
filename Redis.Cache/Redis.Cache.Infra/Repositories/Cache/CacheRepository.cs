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

        public async Task<T> GetValue<T>(Guid id)
        {
            var key = id.ToString().ToLower();

            var result = await _distributedCache.GetStringAsync(key);
            if (result == null)
                return default;

            return JsonConvert.DeserializeObject<T>(result);
        }

        public Task<IEnumerable<T>> GetColletion<T>(string colletionKey)
        {
            throw new NotImplementedException();
        }

        public Task SetColletion<T>(string collectionKey, IEnumerable<T> colletion)
        {
            throw new NotImplementedException();
        }

        public Task SetValue<T>(Guid id, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
