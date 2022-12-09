using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectRPG.Services.UserService;
using ProjectRPG.Models;
using ProjectRPG.Dtos.UserDtos;

namespace ProjectRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _repository;

        public UserController(IUserService repository)
        {
            this._repository = repository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Register(AddUserDto addUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Register(addUser);
            if (result.Success == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Login(UserLoginDto userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.Login(userLogin);
            if (result.Success == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
