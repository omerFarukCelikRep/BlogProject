using DataAccess.EntityFramework.Mapping;
using DataAccess.EntityFramework.Seed;
using Entity.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.EntityFramework.Context
{
    public class BlogDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserFollowedTopic> UserFollowedTopics { get; set; }
        public DbSet<ArticleTopic> ArticleTopics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleMapping());
            builder.ApplyConfiguration(new ArticleTopicMapping());
            builder.ApplyConfiguration(new TopicMapping());
            builder.ApplyConfiguration(new UserFollowedTopicMapping());
            builder.ApplyConfiguration(new AppUserMapping());

            builder.ApplyConfiguration(new AppUserSeed());
            builder.ApplyConfiguration(new IdentityRoleSeed());
            builder.ApplyConfiguration(new UserRoleSeed());

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Added).ToList();

            foreach (var item in modifiedEntries)
            {
                IEntity entity = item.Entity as IEntity;

                if (item != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        //Ekleme İşlemleri
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.ModifiedDate = DateTime.Now;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
