using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [Display(Name = "İsim Soyisim")]
        [MinLength(3, ErrorMessage = "İsminiz en az 3 Karakter Olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Lütfen Bir Email Adresi Giriniz")]
        [Display(Name="Email Adresi")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen Bir Kullanıcı Adı Giriniz")]
        [MinLength(2,ErrorMessage ="Lütfen en az 2 karakter giriniz")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifre Eşleşmemektedir")]
        public string ConfirmedPassword { get; set; }
    }
}
