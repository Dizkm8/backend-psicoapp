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

        public User? MapToUser(RegisterClientDto? registerClientDto)
        {
            if(registerClientDto is null) return null;
            return _mapper.Map<User>(registerClientDto);
        }
    }
}