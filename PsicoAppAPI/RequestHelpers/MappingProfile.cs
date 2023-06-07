using AutoMapper;
using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.Specialist;
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
            CreateMap<FeedPost, FeedPostDto>();
            CreateMap<AvailabilitySlot, AvailabilitySlotDto>();
            CreateMap<AddAvailabilityDto, AvailabilitySlot>();
            CreateMap<Tag, TagDto>();
        }
    }
}