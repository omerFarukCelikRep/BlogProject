using Business.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UI.Views.Shared.Components.UserMenu
{
  
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UserMenuViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);

            UserDetailsDto userDetails = new UserDetailsDto
            {
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture == null ? Array.Empty<byte>(): user.ProfilePicture,
                UserName = user.UserName
            };

            return View(userDetails);
        }
    }
}
