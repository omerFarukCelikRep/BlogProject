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
    public class IdentityRoleSeed : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityRole<Guid>() { Id = Guid.Parse("545B3EDE-AD45-495B-BB35-A787CE7B1732"), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" }
                );
        }
    }
}
