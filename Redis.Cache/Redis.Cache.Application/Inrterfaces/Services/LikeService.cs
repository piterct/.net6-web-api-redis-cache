using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;

namespace Redis.Cache.Application.Inrterfaces.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ICacheRepository _cacheRepository;
        private const string CACHE_COLLETION_KEY = "_AllLikes";

        public LikeService(ILikeRepository likeRepository, ICacheRepository cacheRepository)
        {
            _likeRepository = likeRepository;
            _cacheRepository = cacheRepository;
        }
    } 
}
 