using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    public class UpdateUserDto
    {
        public string Email { get; set; }

        [Required(ErrorMessage ="İsim Boş Geçilemez!")]
        [Display(Name = "İsim Soyisim")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "İsim Boş Geçilemez!")]
        [Display(Name = "Kullanıcı Adı")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Display(Name = "Biyografi")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Display(Name="Profil Resmi")]
        public byte[] ProfilePicture { get; set; }
    }
}
