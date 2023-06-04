using AutoMapper;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.FeedPost;
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
    }
}