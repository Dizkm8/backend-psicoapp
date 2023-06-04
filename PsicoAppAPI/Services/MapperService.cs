using AutoMapper;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.FeedPost;
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

        public FeedPost? MapToFeedPost(AddFeedPostDto? addFeedPostDto)
        {
            if (addFeedPostDto is null) return null;
            return _mapper.Map<FeedPost>(addFeedPostDto);
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