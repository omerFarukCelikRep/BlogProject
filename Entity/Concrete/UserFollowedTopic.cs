using System;

namespace Entity.Concrete
{
    public class UserFollowedTopic
    {
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
