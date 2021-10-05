using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserManager(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task Add(RegisterUserDto registerUserDto)
        {
            try
            {
                AppUser user = new AppUser
                {
                    Email = registerUserDto.Email,
                    Name = registerUserDto.Name,
                    UserName = registerUserDto.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerUserDto.Password);
                if (result.Succeeded)
                {
                    return;
                }
                else
                {
                    string errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += $"Kod : {error.Code}, {error.Description}";
                    }
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Any(Expression<Func<AppUser, bool>> expression)
        {
            return await _userRepository.Any(expression);
        }

        public async Task<bool> Delete(AppUser entity)
        {
            return await _userRepository.Delete(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<AppUser> Get(Expression<Func<AppUser, bool>> expression)
        {
            return await _userRepository.Get(expression);
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<List<AppUser>> GetAll(Expression<Func<AppUser, bool>> expression)
        {
            return await _userRepository.GetAll(expression);
        }

        public async Task<AppUser> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<AppUser> GetByID(Guid id)
        {
            return await _userRepository.GetByID(id);
        }

        public async Task Update(UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(updateUserDto.Name);

                user.Name = updateUserDto.Name;
                user.Biography = updateUserDto.Biography;
                if (updateUserDto.ProfilePicture != null)
                {
                    user.ProfilePicture = updateUserDto.ProfilePicture;
                }
                user.UserName = updateUserDto.UserName;

                await _userManager.UpdateAsync(user);

                await _userManager.UpdateSecurityStampAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<UserDetailsDto> GetUserWithArticles(AppUser user)
        //{

        //}
    }
}
