using DataAccess.Abstract;
using DataAccess.EntityFramework.Abstract;
using DataAccess.EntityFramework.Context;
using Entity.Concrete;

namespace DataAccess.EntityFramework.Repositories
{
    public class EfArticleRepository : EfRepository<Article>, IArticleRepository
    {
        private readonly BlogDbContext _context;

        public EfArticleRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
