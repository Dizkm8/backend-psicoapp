using AutoMapper;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.RegisterClientDto, User>();
            CreateMap<User, ProfileInformationDto>();
            CreateMap<User, UpdateProfileInformationDto>();
            CreateMap<AddFeedPostDto, FeedPost>();
        }
    }
}