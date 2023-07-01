using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;
using Redis.Cache.Application.Inrterfaces.Repositories.Fakes;
using Redis.Cache.Application.Models;

namespace Redis.Cache.Application.Inrterfaces.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ICacheRepository _cacheRepository;
        private readonly IFakeLikeRepository _fakeLikeRepository;
        private const string CACHE_COLLETION_KEY = "_AllLikes";

        public LikeService(ILikeRepository likeRepository,
            ICacheRepository cacheRepository, IFakeLikeRepository fakeLikeRepository)
        {
            _likeRepository = likeRepository;
            _cacheRepository = cacheRepository;
            _fakeLikeRepository = fakeLikeRepository;
        }

        public async Task<Like?> GetLike(Guid id)
        {
            var like = await _cacheRepository.GetValue<Like>(id);

            if (like is null)
            {
                like = await _likeRepository.Get(id);
                await _cacheRepository.SetValue(id, like);
            }

            return like;
        }

        public async Task<IEnumerable<Like?>> GetLikes()
        {
            var likes = await _cacheRepository.GetColletion<Like>(CACHE_COLLETION_KEY);

            if (likes is null || !likes.Any())
            {
                likes = await _fakeLikeRepository.GetLikes();
                await _cacheRepository.SetColletion(CACHE_COLLETION_KEY, likes);
            }

            return likes;
        }

        public async Task RemoveAsync(Like like)
        {
            var cacheLike = await _cacheRepository.GetValue<Like>(like.Id);

            if (cacheLike != null)
            {
                await _cacheRepository.RemoveAsync(like.Id);
            }

            await _likeRepository.RemoveAsync(like);
        }
    }
}
