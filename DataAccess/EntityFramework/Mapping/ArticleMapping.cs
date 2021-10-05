using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityFramework.Mapping
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(a => a.Image).IsRequired();
            builder.Property(a => a.IsPublished).IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(a => a.Articles)
                .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.ArticleTopics).WithOne(a => a.Article).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
