using Entity.Concrete;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByEmail(string email);
    }
}
