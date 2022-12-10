using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectRPG.Dtos.UserDtos;
using ProjectRPG.Models;

namespace ProjectRPG.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> Register(AddUserDto addUser);

        Task<bool> UserExists(string username);

        Task<ServiceResponse<UserLoginSuccessDto>> Login(UserLoginDto userLogin);
    }
}
