using Microsoft.EntityFrameworkCore;
using Redis.Cache.Application.Models;

namespace Redis.Cache.Infra.DbContexts
{
    public class LikeDbContext : DbContext
    {
        public LikeDbContext(DbContextOptions<LikeDbContext> options) : base(options) { }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(t => t.Id);

            builder.Entity<Like>().Property(x => x.Name)
           .IsRequired();
        }
    }
}
