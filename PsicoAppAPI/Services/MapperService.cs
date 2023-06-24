using AutoMapper;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.Appointment;
using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public AvailabilitySlotDto? MapToAvailabilitySlotDto(AvailabilitySlot? availabilitySlot)
        {
            if (availabilitySlot is null) return null;
            return _mapper.Map<AvailabilitySlotDto>(availabilitySlot);
        }

        public FeedPost? MapToFeedPost(AddFeedPostDto? addFeedPostDto)
        {
            if (addFeedPostDto is null) return null;
            return _mapper.Map<FeedPost>(addFeedPostDto);
        }

        public FeedPostDto? MapToFeedPostDto(FeedPost? feedPost)
        {
            if (feedPost is null) return null;
            return _mapper.Map<FeedPostDto>(feedPost);
        }

        public IEnumerable<AvailabilitySlot>? MapToListOfAvailabilitySlot(
            IEnumerable<AddAvailabilityDto>? availabilities,
            string userId)
        {
            if (availabilities is null) return null;
            var mappedAvailabilities = availabilities.Select(x =>
            {
                var availabilitySlot = _mapper.Map<AvailabilitySlot>(x);
                availabilitySlot.UserId = userId;
                availabilitySlot.EndTime = availabilitySlot.StartTime.AddHours(1);
                availabilitySlot.IsAvailableOverride = true;
                return availabilitySlot;
            });
            return mappedAvailabilities;
        }

        public List<AvailabilitySlotDto>? MapToListOfAvailabilitySlotDto(List<AvailabilitySlot>? availabilitySlots)
        {
            if (availabilitySlots is null) return null;
            var mappedDto = availabilitySlots.Select(x => _mapper.Map<AvailabilitySlotDto>(x)).ToList();
            return mappedDto;
        }

        public ProfileInformationDto? MapToProfileInformationDto(User? user)
        {
            if (user is null) return null;
            return _mapper.Map<ProfileInformationDto>(user);
        }

        public IEnumerable<TagDto> MapToTagDto(IEnumerable<Tag>? tags)
        {
            if (tags is null) return new List<TagDto>();
            var mappedTags = tags.Select(x => _mapper.Map<TagDto>(x));
            return mappedTags ?? new List<TagDto>();
        }

        public ForumPost MapToForumPost(AddForumPostDto? postDto)
        {
            return postDto is null ? new ForumPost() : _mapper.Map<ForumPost>(postDto);
        }

        public ForumPostDto? MapToForumPostDto(ForumPost? post)
        {
            return post is null ? null : _mapper.Map<ForumPostDto>(post);
        }

        public UpdateProfileInformationDto? MapToUpdatedProfileInformationDto(User? user)
        {
            if (user is null) return null;
            return _mapper.Map<UpdateProfileInformationDto>(user);
        }

        public User? MapToUser(RegisterClientDto? registerClientDto)
        {
            if (registerClientDto is null) return null;
            return _mapper.Map<User>(registerClientDto);
        }

        public List<ForumPostDto> MapToForumPostDto(List<ForumPost>? posts)
        {
            var mappedPosts = posts?.Select(x => _mapper.Map<ForumPostDto>(x)).ToList();
            return mappedPosts ?? new List<ForumPostDto>();
        }

        public List<FeedPostDto> MapToFeedPostDto(List<FeedPost>? posts)
        {
            var mappedPosts = posts?.Select(x => _mapper.Map<FeedPostDto>(x)).ToList();
            return mappedPosts ?? new List<FeedPostDto>();
        }

        public List<SpecialistAppointmentDto> MapToSpecialistAppointmentDto(List<Appointment>? appointments)
        {
            var mappedAppointments = appointments?.Select(x => _mapper.Map<SpecialistAppointmentDto>(x)).ToList();
            return mappedAppointments ?? new List<SpecialistAppointmentDto>();
        }

        public List<ClientAppointmentDto> MapToClientAppointmentDto(List<Appointment>? appointments)
        {
            var mappedAppointments = appointments?.Select(x => _mapper.Map<ClientAppointmentDto>(x)).ToList();
            return mappedAppointments ?? new List<ClientAppointmentDto>();
        }
    }
}