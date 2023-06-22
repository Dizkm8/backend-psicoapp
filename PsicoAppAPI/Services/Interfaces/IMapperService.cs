using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.DTOs.Specialist;
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
        /// <summary>
        /// Maps the attributes of a AvailabilitySlot to a AvailabilitySlotDto
        /// </summary>
        /// <param name="availabilitySlot">AvailabilitySlot to map</param>
        /// <returns>AvailabilitySlotDto mapped. Null if cannot be mapped</returns>
        public AvailabilitySlotDto? MapToAvailabilitySlotDto(AvailabilitySlot? availabilitySlot);
        /// <summary>
        /// Maps a list of AvailabilitySlot to a list of AvailabilitySlotDto
        /// </summary>
        /// <param name="availabilitySlots">List of availabilites to map</param>
        /// <returns>List mapped. Null if cannot be mapped</returns>
        public List<AvailabilitySlotDto>? MapToListOfAvailabilitySlotDto(List<AvailabilitySlot>? availabilitySlots);
        /// <summary>
        /// Maps a list of AddAvailabilityDto to a list of AvailabilitySlot
        /// The Endtime are the StartTime + 1 hour
        /// All the instances of AvailabilitySlot will have the same UserId
        /// also all the instance will base IsAvailable in true
        /// </summary>
        /// <param name="availabilities">IEnumerable with Availabilities</param>
        /// <param name="userId">UserId to set in all the instances</param>
        /// <returns>IEnumerable of availabilities mapped, null if cannot be mapped</returns>
        public IEnumerable<AvailabilitySlot>? MapToListOfAvailabilitySlot(IEnumerable<AddAvailabilityDto>? availabilities
            , string userId);
        /// <summary>
        /// Maps a IEnumerable of Tag to a IEnumerable of TagDto
        /// </summary>
        /// <param name="tags">IEnumerable with tags</param>
        /// <returns>IEnumerable of tags, null if cannot be mapped</returns>
        public IEnumerable<TagDto> MapToTagDto(IEnumerable<Tag>? tags);
        /// <summary>
        /// Maps the attributes of a AddForumPostDto to a ForumPost
        /// ForumPost will have:
        /// PublishedOn attribute not mapped
        /// UserId attribute not mapped
        /// </summary>
        /// <param name="postDto">Dto source</param>
        /// <returns>ForumPost mapped or null</returns>
        public ForumPost MapToForumPost(AddForumPostDto? postDto);
        /// <summary>
        /// Maps the attributes of a ForumPost to a ForumPostDto
        /// </summary>
        /// <param name="post">ForumPost to map</param>
        /// <returns>FeedPost mapped. Null if cannot be mapped</returns>
        public ForumPostDto? MapToForumPostDto(ForumPost? post);
    }
}