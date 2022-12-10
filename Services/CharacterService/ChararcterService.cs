using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectRPG.Data;
using ProjectRPG.Dtos.CharacterDtos;
using ProjectRPG.Models;

namespace ProjectRPG.Services.CharacterService
{
    public class ChararcterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character
            {
                Name = "Sam",
                Class = RpgClass.Mage,
                Id = 1
            }
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ChararcterService(IMapper mapper, DataContext context)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Add(AddCharacterDto addCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var newCharacter = _mapper.Map<Character>(addCharacter);
            await _context.Characters.AddAsync(newCharacter);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(newCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
                .Where(c => c.User.Id == userId)
                .ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.SingleOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(
            UpdateCharacterDto updateCharacter
        )
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var dbCharacter = await _context.Characters.SingleOrDefaultAsync(
                c => c.Id == updateCharacter.Id
            );
            if (dbCharacter == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "not found";
                return serviceResponse;
            }

            _mapper.Map(updateCharacter, dbCharacter);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

            return serviceResponse;
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var serviceResponse = new ServiceResponse();
            var dbCharacter = await _context.Characters.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCharacter == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "not found";
                return serviceResponse;
            }
            _context.Characters.Remove(dbCharacter);
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
    }
}
