using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class ArticleTopic
    {
        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
