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
    public class UserRoleSeed : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityUserRole<Guid>() { RoleId = Guid.Parse("545B3EDE-AD45-495B-BB35-A787CE7B1732"), UserId = Guid.Parse("B7010A89-68CA-40E7-8C9D-A7B44BEA9028") }
                );
        }
    }
}
