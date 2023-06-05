using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class FeedPostManagementService : IFeedPostManagementService
    {
        private readonly IFeedPostService _feedPostService;
        private readonly IAuthService _authService;
        private readonly IMapperService _mapperService;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;
        private readonly IOpenAIService _openAIService;

        public FeedPostManagementService(IFeedPostService feedPostService, IAuthService authService,
            IMapperService mapperService, IUserService userService, ITagService tagService,
            IOpenAIService openAIService)
        {
            _feedPostService = feedPostService;
            _authService = authService;
            _mapperService = mapperService;
            _userService = userService;
            _tagService = tagService;
            _openAIService = openAIService;
        }

        public async Task<FeedPostDto?> AddFeedPost(AddFeedPostDto feedPostDto)
        {
            var userId = _authService.GetUserIdInToken();
            if (userId is null) return null;
            var isSpecialist = await ValidateSpecialist(userId);
            if (!isSpecialist) return null;

            var feedPost = _mapperService.MapToFeedPost(feedPostDto);
            if (feedPost is null) return null;
            // Update properties not mapped
            feedPost.UserId = userId;
            feedPost.PublishedOn = DateOnly.FromDateTime(DateTime.Now);

            var result = await _feedPostService.AddFeedPost(feedPost);
            if (!result) return null;

            var postDto = _mapperService.MapToFeedPostDto(feedPost);
            return postDto;
        }

        public async Task<bool> CheckPostContext(AddFeedPostDto feedPostDto)
        {
            var content = feedPostDto.Content;
            if (content is null) return false;
            var result = await _openAIService.CheckPostContent(content);
            return result;
        }

        public async Task<bool> CheckPostTag(AddFeedPostDto feedPostDto)
        {
            var result = await _tagService.ExistsTagById(feedPostDto.TagId);
            return result;
        }

        private async Task<bool> ValidateSpecialist(string userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null) return false;
            var specialistRoleId = await _userService.GetIdOfSpecialistRole();
            return user.RoleId == specialistRoleId;
        }
    }
}