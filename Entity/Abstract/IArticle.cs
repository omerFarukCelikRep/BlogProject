using Entity.Concrete;
using System.Collections.Generic;

namespace Entity.Abstract
{
    public interface IArticle : IEntity
    {
        string Title { get; set; }
        string Content { get; set; }
        int ReadingCount { get; set; }
        int ReadTime { get; set; }
        byte[] Image { get; set; }
        bool IsPublished { get; set; }
        int LikeCount { get; set; }

        AppUser User { get; set; }
        ICollection<ArticleTopic> ArticleTopics { get; set; }
    }
}
