using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectRPG.Dtos.CharacterDtos;
using ProjectRPG.Dtos.UserDtos;
using ProjectRPG.Models;

namespace ProjectRPG.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
        #region  Character
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
        #endregion

        #region User
            CreateMap<User, GetUserDto>();
        #endregion
        }
    }
}
