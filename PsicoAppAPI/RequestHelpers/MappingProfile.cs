using AutoMapper;
using PsicoAppAPI.DTOs.Appointment;
using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.DTOs.User;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterClientDto, User>();
            CreateMap<User, ProfileInformationDto>();
            CreateMap<User, UpdateProfileInformationDto>();
            CreateMap<AddFeedPostDto, FeedPost>();
            CreateMap<FeedPost, FeedPostDto>();
            CreateMap<AvailabilitySlot, AvailabilitySlotDto>();
            CreateMap<AddAvailabilityDto, AvailabilitySlot>();
            CreateMap<Tag, TagDto>();
            CreateMap<AddForumPostDto, ForumPost>();
            CreateMap<ForumPost, ForumPostDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Appointment, SpecialistAppointmentDto>();
            CreateMap<Appointment, ClientAppointmentDto>();
            CreateMap<User, RegisterSpecialistDto>();
            CreateMap<RegisterSpecialistDto, User>();
            CreateMap<Speciality, SpecialityDto>();
            CreateMap<User, UserDto>();
            CreateMap<Specialist, SpecialistDto>();
        }
    }
}