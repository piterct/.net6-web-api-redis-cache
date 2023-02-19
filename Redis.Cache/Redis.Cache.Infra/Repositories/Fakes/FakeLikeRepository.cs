using Redis.Cache.Application.Inrterfaces.Repositories.Fakes;
using Redis.Cache.Application.Models;

namespace Redis.Cache.Infra.Repositories.Fakes
{
    public  class FakeLikeRepository : IFakeLikeRepository
    {

        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await Task.FromResult(GenerateFakeLikes());
        }

        private IEnumerable<Like> GenerateFakeLikes()
        {
            return Enumerable.Range(1, 20).Select(index => new Like($"Like {index}"));
        }
    }
}
