using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectRPG.Dtos.CharacterDtos;
using ProjectRPG.Models;

namespace ProjectRPG.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<GetCharacterDto>> GetById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
        Task<ServiceResponse<GetCharacterDto>> Add(AddCharacterDto addCharacter);

        Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter);

        Task<ServiceResponse> Delete(int id);
    }
}
