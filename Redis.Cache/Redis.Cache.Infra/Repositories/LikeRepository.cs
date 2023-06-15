using Microsoft.EntityFrameworkCore;
using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Models;
using Redis.Cache.Infra.DbContexts;
using System.Xml.Linq;

namespace Redis.Cache.Infra.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly LikeDbContext _likeDbContext;

        public LikeRepository(LikeDbContext likeDbContext)
        {
            _likeDbContext = likeDbContext;
        }

        public async Task<Like> Add(Like like)
        {
            await _likeDbContext.Likes.AddAsync(like);
            await _likeDbContext.SaveChangesAsync();
            return like;
        }

        public async Task<Like?> Get(Guid id)
        {
            var like = await _likeDbContext.Likes.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            return like;
        }

        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await Task.FromResult(GenerateFakeLikes());
        }

        private IEnumerable<Like> GenerateFakeLikes()
        {
            return Enumerable.Range(1, 20).Select(index => new Like($"Like {index}"));
        }

        public async Task RemoveAsync(Like like)
        {
            _likeDbContext.Remove(like);
            await _likeDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _likeDbContext?.Dispose();
        }
    }
}
