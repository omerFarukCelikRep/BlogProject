using Entity.Abstract;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class Topic : ITopic
    {
        public Topic()
        {
            ArticleTopics = new HashSet<ArticleTopic>();
            UserFollowedTopics = new HashSet<UserFollowedTopic>();
        }
        public Guid Id { get; set; }
        private DateTime _createdDate = DateTime.Now;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }


        private Status _status = Status.Added;
        public Status Status { get => _status; set => _status = value; }

        [Required]
        [Display(Name = "Topic Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Topic Image")]
        public byte[] Image { get; set; }

        //Navigation Prop.
        public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
        public virtual ICollection<UserFollowedTopic> UserFollowedTopics { get; set; }
    }
}