using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectRPG.Dtos.CharacterDtos;
using ProjectRPG.Models;
using ProjectRPG.Services.CharacterService;

namespace ProjectRPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _repository;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Get(int id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Create(
            [FromBody] AddCharacterDto character
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _repository.Add(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Create(
            [FromBody] UpdateCharacterDto updateCharacter
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _repository.Update(updateCharacter);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse>> Delete(int id)
        {
            var response = await _repository.Delete(id);
            if (!response.Success)
                return NotFound();
            return Ok();
        }
    }
}
