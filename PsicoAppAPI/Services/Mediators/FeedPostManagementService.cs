using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class FeedPostManagementService : IFeedPostManagementService
    {
        private readonly IFeedPostService _feedPostService;
        private readonly IAuthService _authService;

        public FeedPostManagementService(IFeedPostService feedPostService, IAuthService authService)
        {
            _feedPostService = feedPostService;
            _authService = authService;
        }

        public Task<bool> AddFeedPost(AddFeedPostDto? feedPostDto)
        {
            throw new NotImplementedException();
        }
    }
}