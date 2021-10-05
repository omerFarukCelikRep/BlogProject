using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.CustomValidations
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password.Length < 8)
                errors.Add(new IdentityError { Code = "PasswordLength", Description = "Lütfen şifreyi en az 8 karakter giriniz." });
            if (password.ToLower().Contains(user.UserName.ToLower()))
                errors.Add(new IdentityError { Code = "PasswordContainsUserName", Description = "Lütfen şifre içerisinde kullanıcı adınızı yazmayınız." });

            if (!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
