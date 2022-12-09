using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectRPG.Dtos.Character;
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

        public ChararcterService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Add(AddCharacterDto addCharacter)
        {
            var newCharacter = _mapper.Map<Character>(addCharacter);
            newCharacter.Id = characters.Count;
            characters.Add(newCharacter);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(newCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var character = characters.SingleOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(
            UpdateCharacterDto updateCharacter
        )
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var foundCharacter = characters.SingleOrDefault(c => c.Id == updateCharacter.Id);
            if (foundCharacter == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "not found";
                return serviceResponse;
            }

            _mapper.Map(updateCharacter, foundCharacter);

            serviceResponse.Data = _mapper.Map<Character, GetCharacterDto>(foundCharacter);

            return serviceResponse;
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var serviceResponse = new ServiceResponse();
            var character = characters.Find(c => c.Id == id);
            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "not found";
                return serviceResponse;
            }
            characters.Remove(character);
            return serviceResponse;
        }
    }
}
