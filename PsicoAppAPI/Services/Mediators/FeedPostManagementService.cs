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

        public FeedPostManagementService(IFeedPostService feedPostService, IAuthService authService,
            IMapperService mapperService, IUserService userService)
        {
            _feedPostService = feedPostService;
            _authService = authService;
            _mapperService = mapperService;
            _userService = userService;
        }

        public async Task<bool> AddFeedPost(AddFeedPostDto? feedPostDto)
        {
            var userId = _authService.GetUserIdInToken();
            if(userId is null) return false;
            var isSpecialist = await ValidateSpecialist(userId);
            if(!isSpecialist) return false;

            var mappedPost = _mapperService.MapToFeedPost(feedPostDto);
            if (mappedPost is null) return false;
            mappedPost.UserId = userId;
            mappedPost.PublishedOn = DateOnly.FromDateTime(DateTime.Now);
            mappedPost.TagId = 1; //CURRENTLY HARDCODED!!

            var result = await _feedPostService.AddFeedPost(mappedPost);
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