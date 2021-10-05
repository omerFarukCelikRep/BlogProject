using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    public class CreateArticleDto
    {
        [Required(ErrorMessage = "Başlık Zorunlu Alandır.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik Zorunlu Alandır.")]
        [Display(Name = "İçerik")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Resim")]
        public byte[] Image { get; set; }

        public AppUser User { get; set; }

        [Required(ErrorMessage ="En Az Bir Konu Seçiniz!")]
        [Display(Name ="Konu")]
        public List<Guid> TopicIds { get; set; }
    }
}
