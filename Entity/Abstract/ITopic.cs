using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Abstract
{
    public interface ITopic : IEntity
    {
        string Name { get; set; }
        byte[] Image { get; set; }
        ICollection<ArticleTopic> ArticleTopics { get; set; }
        ICollection<UserFollowedTopic> UserFollowedTopics { get; set; }
    }
}
