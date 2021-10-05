using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityFramework.Mapping
{
    public class UserFollowedTopicMapping : IEntityTypeConfiguration<UserFollowedTopic>
    {
        public void Configure(EntityTypeBuilder<UserFollowedTopic> builder)
        {
            builder.HasKey(mt => new { mt.UserId, mt.TopicId });

            builder.HasOne(mt => mt.User)
                .WithMany(m => m.UserFollowedTopics)
                .HasForeignKey(mt => mt.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(mt => mt.Topic)
                .WithMany(t => t.UserFollowedTopics)
                .HasForeignKey(mt => mt.TopicId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
