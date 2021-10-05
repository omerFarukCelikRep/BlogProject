using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityFramework.Mapping
{
    public class ArticleTopicMapping : IEntityTypeConfiguration<ArticleTopic>
    {
        public void Configure(EntityTypeBuilder<ArticleTopic> builder)
        {
            builder.HasKey(at => new { at.TopicId, at.ArticleId });

            builder.HasOne(t => t.Topic)
                .WithMany(at => at.ArticleTopics)
                .HasForeignKey(at => at.TopicId).OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(t => t.Article)
                .WithMany(at => at.ArticleTopics)
                .HasForeignKey(at => at.ArticleId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
