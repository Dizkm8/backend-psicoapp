using AutoMapper;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterClientDto, User>();
            CreateMap<User, ProfileInformationDto>();
        }
    }
}