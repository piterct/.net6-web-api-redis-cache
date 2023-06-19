namespace Redis.Cache.Application.Inrterfaces.Repositories.Cache
{
    public interface ICacheRepository
    {
        Task<T?> GetValue<T>(Guid id);
        Task<IEnumerable<T?>?> GetColletion<T>(string colletionKey);
        Task SetValue<T>(Guid id, T obj);
        Task SetColletion<T>(string collectionKey, IEnumerable<T> colletion);
        Task RemoveAsync(Guid id);
    }
}
