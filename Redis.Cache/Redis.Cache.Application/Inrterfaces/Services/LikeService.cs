using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;
using Redis.Cache.Application.Models;

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

        private async Task<Like> GetLike(Guid id)
        {
            var like = await _cacheRepository.GetValue<Like>(id);

            if (like is null)
            {
                like = await _likeRepository.Get(id);
                await _cacheRepository.SetValue(id, like);
            }

            return like;
        }
    }
}
