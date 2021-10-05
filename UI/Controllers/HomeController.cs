using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyfService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, INotyfService notyfService, IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        [Route("/Login")]
        public IActionResult Login(string ReturnUrl)
        {
            TempData["returnUrl"] = ReturnUrl;

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([Bind("Email,Password,Lock")]LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(loginUserDto.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginUserDto.Password, false, loginUserDto.Lock);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);

                        if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        return Redirect(TempData["returnUrl"].ToString());
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);

                        int failcount = await _userManager.GetAccessFailedCountAsync(user);
                        if (failcount == 5)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(5)));
                            _notyfService.Error("Art arda 5 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 5 dk kitlenmiştir.");
                        }
                        else
                        {
                            if (result.IsLockedOut)
                                _notyfService.Error("Art arda 5 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 5 dk kilitlenmiştir.");
                            else
                                _notyfService.Error("E-posta veya şifre yanlış.");
                        }

                    }
                }
                else
                {
                    _notyfService.Error("Böyle bir kullanıcı bulunmamaktadır.");
                    _notyfService.Error("E-posta veya şifre yanlış.");
                }
            }
            return View(loginUserDto);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register([Bind("Name,Email,UserName,Password,ConfirmedPassword")]RegisterUserDto registerUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.Add(registerUserDto);

                    return RedirectToAction(nameof(Index));
                }

                return View(registerUserDto);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(registerUserDto);

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
