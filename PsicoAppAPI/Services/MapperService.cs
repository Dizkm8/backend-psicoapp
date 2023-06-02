using AutoMapper;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public User MapAttributesToUser(UpdateProfileInformationDto profileInformationDto, User user)
        {
            user.Name = profileInformationDto.Name;
            user.FirstLastName = profileInformationDto.FirstLastName;
            user.SecondLastName = profileInformationDto.SecondLastName;
            user.Email = profileInformationDto.Email;
            user.Gender = profileInformationDto.Gender;
            user.Phone = profileInformationDto.Phone;
            return user;
        }

        public User? MapToUser(RegisterClientDto? registerClientDto)
        {
            if (registerClientDto is null) return null;
            return _mapper.Map<User>(registerClientDto);
        }
    }
}