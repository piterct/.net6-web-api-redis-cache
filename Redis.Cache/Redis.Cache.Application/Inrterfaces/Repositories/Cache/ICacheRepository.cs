namespace Redis.Cache.Application.Inrterfaces.Repositories.Cache
{
    public interface ICacheRepository
    {
        Task<T?> GetValue<T>(Guid id);
        Task<IEnumerable<T?>?> GetColletion<T>(string colletionKey);
        Task SetValue<T>(Guid id, T obj, int absoluteExpirationRelativeToNow = 300, int slidingExpiration = 300);
        Task SetColletion<T>(string collectionKey, IEnumerable<T> colletion, int absoluteExpirationRelativeToNow = 300, int slidingExpiration = 300);
        Task RemoveAsync(Guid id);
    }
}
