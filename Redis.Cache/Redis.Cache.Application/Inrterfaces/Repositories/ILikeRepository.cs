using Redis.Cache.Application.Models;

namespace Redis.Cache.Application.Inrterfaces.Repositories
{
    public interface ILikeRepository : IRepository
    {
        Task<Like> Add(Like like);
        Task<Like?> Get(Guid id);
        Task<IEnumerable<Like>> GetLikes();
        Task RemoveAsync(Like like);
    }
}
