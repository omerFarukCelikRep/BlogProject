using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly INotyfService _notyfService;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, INotyfService notyfService, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _notyfService = notyfService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);

            UserDetailsDto userDetails = new UserDetailsDto
            {
                Name = user.Name,
                Biography = user.Biography,
                ProfilePicture = user.ProfilePicture
            };

            return View(userDetails);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            UpdateUserDto userDetails = new UpdateUserDto
            {
                Name = user.Name,
                Biography = user.Biography,
                ProfilePicture = user.ProfilePicture == null ? Array.Empty<byte>() : user.ProfilePicture,
                UserName = user.UserName
            };

            return View(userDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Name,Biography,UserName,ProfilePicture")] UpdateUserDto updateUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();

                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            updateUserDto.ProfilePicture = dataStream.ToArray();
                        }
                    }

                    await _userService.Update(updateUserDto);

                    var user = await _userManager.GetUserAsync(User);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, false);
                    _notyfService.Success("Kullanıcı Bilgileri Güncellendi");
                    return RedirectToAction(nameof(Details));
                }

                return View(updateUserDto);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(updateUserDto);
            }
        }


    }
}
