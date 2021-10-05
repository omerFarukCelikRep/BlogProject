using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.CustomValidations
{
    public class CustomUserValidatior : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (int.TryParse(user.UserName[0].ToString(), out int _))
                errors.Add(new IdentityError { Code = "UserNameNumberStartWith", Description = "Kullanıcı adı sayısal ifadeyle başlayamaz!" });
            if (user.UserName.Length < 3)
                errors.Add(new IdentityError { Code = "UserNameLength", Description = "Kullanıcı adı 3 - 15 karakter arasında olmalıdır..." });
            if (user.Email.Length > 85)
                errors.Add(new IdentityError { Code = "EmailLength", Description = "Email 85 karakterden fazla olamaz..." });

            if (!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
