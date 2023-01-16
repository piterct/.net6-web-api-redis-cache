using Microsoft.EntityFrameworkCore;
using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Models;
using Redis.Cache.Infra.DbContexts;

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

        public async Task<Like> Get(Guid id)
        {
            var like = await _likeDbContext.Likes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return like;
        }

        public void Dispose()
        {
            _likeDbContext?.Dispose();
        }
    }
}
