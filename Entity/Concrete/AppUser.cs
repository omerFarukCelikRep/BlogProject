using Entity.Abstract;
using Entity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class AppUser : IdentityUser<Guid>, IAppUser
    {
        public AppUser()
        {
            Articles = new HashSet<Article>();
            UserFollowedTopics = new HashSet<UserFollowedTopic>();
        }
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
        [Display(Name = "İsim Soyisim")]
        [MinLength(3, ErrorMessage = "İsminiz min 3 Karakter Olmalıdır")]
        public string Name { get; set; }

        [Display(Name = "Biyografi")]
        [StringLength(180, ErrorMessage = "Biyografiniz max. 180 karakter olmalıdır.")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<UserFollowedTopic> UserFollowedTopics { get; set; }
    }
}
