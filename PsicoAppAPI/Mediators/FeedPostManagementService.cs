using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators
{
    public class FeedPostManagementService : PostManagementService, IFeedPostManagementService
    {
        private readonly IFeedPostService _feedPostService;
        private readonly IAuthManagementService _authService;
        private readonly IMapperService _mapperService;
        private readonly ITagService _tagService;

        public FeedPostManagementService(IFeedPostService feedPostService, IAuthManagementService authService,
            IMapperService mapperService, ITagService tagService) : 
            base(feedPostService, authService, mapperService, tagService)
        {
            _feedPostService = feedPostService ?? throw new ArgumentNullException(nameof(feedPostService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
        }

        public async Task<FeedPostDto?> AddFeedPost(AddFeedPostDto feedPostDto)
        {
            var user = await _authService.GetUserEnabledAndSpecialistFromToken();
            if (user is null) return null;
            var userId = user.Id;

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
    }
}