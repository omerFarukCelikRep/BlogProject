using Entity.Concrete;
using System.Collections.Generic;

namespace Entity.Abstract
{
    public interface IAppUser : IEntity
    {
        string Name { get; set; }
        string Biography { get; set; }
        byte[] ProfilePicture { get; set; }
        ICollection<Article> Articles { get; set; }
        ICollection<UserFollowedTopic> UserFollowedTopics { get; set; }
    }
}
