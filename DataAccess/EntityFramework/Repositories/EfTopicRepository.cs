using DataAccess.Abstract;
using DataAccess.EntityFramework.Abstract;
using DataAccess.EntityFramework.Context;
using Entity.Concrete;

namespace DataAccess.EntityFramework.Repositories
{
    public class EfTopicRepository : EfRepository<Topic>, ITopicRepository
    {
        private readonly BlogDbContext _context;

        public EfTopicRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
