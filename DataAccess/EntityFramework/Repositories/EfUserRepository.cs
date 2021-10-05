using DataAccess.Abstract;
using DataAccess.EntityFramework.Abstract;
using DataAccess.EntityFramework.Context;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Repositories
{
    public class EfUserRepository : EfRepository<AppUser>, IUserRepository
    {
        private readonly BlogDbContext _context;

        public EfUserRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByEmail(string email)
        {
            return await Get(a => a.Email == email);
        }
    }
}
