using Entity.Concrete;
using Entity.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService : IService<AppUser>
    {
        Task Add(RegisterUserDto registerUserDto);
        Task Update(UpdateUserDto updateUserDto);
        Task<AppUser> GetByEmail(string email);
    }
}
