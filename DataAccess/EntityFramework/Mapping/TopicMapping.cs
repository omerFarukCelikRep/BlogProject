using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityFramework.Mapping
{
    public class TopicMapping : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Image)
                .IsRequired(false); 

            builder.Property(tp => tp.Name)
                .IsRequired();

            builder.HasMany(a => a.UserFollowedTopics).WithOne(a => a.Topic).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(a => a.ArticleTopics).WithOne(a => a.Topic).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
