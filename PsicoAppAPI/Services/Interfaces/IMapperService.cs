using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IMapperService
    {
         /// <summary>
        /// Maps a RegisterClientDto to a User
        /// </summary>
        /// <param name="registerClientDto">Dto to map</param>
        /// <returns>User mapped. null if cannot be mapped</returns>
        public User? MapToUser(RegisterClientDto? registerClientDto);   
        /// <summary>
        /// Maps the attributes of a UpdateProfileInformationDto to a User
        /// The Dto dont have all the attributes of the User,
        /// so map only affects the attributes that the Dto have
        /// This function dont use AutoMapper to avoid overwrite existing attributes
        /// </summary>
        /// <param name="profileInformationDto">Source Dto</param>
        /// <param name="user">User destination</param>
        /// <returns>User mapped</returns>
        public User MapAttributesToUser(UpdateProfileInformationDto profileInformationDto, User user);
        /// <summary>
        /// Maps the user attributes to a ProfileInformationDto
        /// None attribute of Dto returned is null
        /// </summary>
        /// <param name="user">User soure</param>
        /// <returns>Dto mapped</returns>
        public UpdateProfileInformationDto? MapToUpdatedProfileInformationDto(User? user);
        /// <summary>
        /// Maps the user attributes to a ProfileInformationDto
        /// None attribute of Dto returned is null
        /// </summary>
        /// <param name="user">User source</param>
        /// <returns>Dto mapped</returns>
        public ProfileInformationDto? MapToProfileInformationDto(User? user);
        /// <summary>
        /// Maps the attributes of a AddFeedPostDto to a FeedPost
        /// Feedpost will have:
        /// PublishedOn attribute not mapped
        /// UserId attribute not mapped
        /// </summary>
        /// <param name="addFeedPostDto">Dto source</param>
        /// <returns>FeedPost mapped or null</returns>
        public FeedPost? MapToFeedPost(AddFeedPostDto? addFeedPostDto);
        /// <summary>
        /// Maps the attributes of a FeedPost to a FeedPostDto
        /// </summary>
        /// <param name="feedPost">Feedpost to map</param>
        /// <returns>FeedPost mapped. Null if cannot be mapped</returns>
        public FeedPostDto? MapToFeedPostDto(FeedPost? feedPost);
    }
}