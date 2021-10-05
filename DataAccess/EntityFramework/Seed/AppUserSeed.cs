using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Seed
{
    public class AppUserSeed : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            AppUser user = new AppUser
            {
                Id = Guid.Parse("B7010A89-68CA-40E7-8C9D-A7B44BEA9028"),
                Name = "Admin",
                UserName = "Admin",
                Email = "admin@email.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();

            user.PasswordHash = passwordHasher.HashPassword(user, "newPassword0+");

            builder.HasData(user);
        }
    }
}
