using Redis.Cache.Application.Models;

namespace Redis.Cache.Application.Inrterfaces.Services
{
    public interface ILikeService
    {
        Task<Like?> GetLike(Guid id);
        Task<IEnumerable<Like?>> GetLikes();
    }
}
