using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectRPG.Data;
using ProjectRPG.Dtos.UserDtos;
using ProjectRPG.Models;

namespace ProjectRPG.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<GetUserDto>> Login(UserLoginDto userLogin)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var dbUser = await _context.Users.SingleOrDefaultAsync(
                u => u.Username.ToLower().Equals(userLogin.Username.ToLower())
            );
            if (dbUser == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "user not found";
                return serviceResponse;
            }
            if (!VerifyPasswordHash(userLogin.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "wrong password";
            }
            serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> Register(AddUserDto addUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            if (await UserExists(addUser.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "user already exists";
                return serviceResponse;
            }
            CreatePasswordHash(addUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = addUser.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
                return true;

            return false;
        }

        private void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
