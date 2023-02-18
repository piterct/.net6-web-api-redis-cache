using Redis.Cache.Application.Models;

namespace Redis.Cache.Application.Inrterfaces.Repositories.Fakes
{
    public interface IFakeLikeRepository
    {
        Task<IEnumerable<Like>> GetLikes();
    }
}
