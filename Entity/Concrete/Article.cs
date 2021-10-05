using Entity.Abstract;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class Article : IArticle
    {
        public Article()
        {
            ArticleTopics = new HashSet<ArticleTopic>();
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

        [Required(ErrorMessage = "Başlık Zorunlu Alandır.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik Zorunlu Alandır.")]
        [Display(Name = "İçerik")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int ReadingCount { get; set; }
        public int ReadTime { get; set; }

        [Required(ErrorMessage ="Resim Zorunludur!")]
        [Display(Name ="Resim")]
        public byte[] Image { get; set; }
        public bool IsPublished { get; set; }
        public int LikeCount { get; set; }

        //Navigation Prop.
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
    }
}
