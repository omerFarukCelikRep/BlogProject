using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityFramework.Mapping
{
    public class AppUserMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.Email)
                .IsRequired();

            builder.Property(m => m.UserName)
                .IsRequired(false);

            builder.HasMany(a => a.Articles).WithOne(a => a.User).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(a => a.UserFollowedTopics).WithOne(a => a.User).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
