using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DataAccess.EntityFramework.Context;
using Entity.Concrete;
using Entity.CustomValidations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UI.Extensions;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BlogConnectionString")));

            services.AddIdentity<AppUser, IdentityRole<Guid>>(x =>
            {
                x.Password.RequiredLength = 8;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = true;
                x.Password.RequireUppercase = true;
                x.Password.RequireDigit = true;

                x.User.RequireUniqueEmail = true;
                x.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
            })
                .AddPasswordValidator<CustomPasswordValidator>()
                .AddUserValidator<CustomUserValidatior>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Login");
                x.LogoutPath = new PathString("/Logout");
                x.Cookie = new CookieBuilder
                {
                    Name = "BlogAppCookie",
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                x.SlidingExpiration = true;
                x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                x.AccessDeniedPath = new PathString("/AccessDenied");
            });

            services.AddNotyf(options =>
            {
                options.DurationInSeconds = 10;
                options.IsDismissable = true;
                options.Position = NotyfPosition.TopCenter;
            });

            services.AddControllersWithViews();

            services.Scoped();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNotyf();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
