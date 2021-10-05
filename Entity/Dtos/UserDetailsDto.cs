using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    public class UserDetailsDto
    {
        [Display(Name = "İsim Soyisim")]
        public string Name { get; set; }

        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name="Email Adresi")]
        public string Email { get; set; }

        [Display(Name = "Biyografi")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
