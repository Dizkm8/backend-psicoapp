using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class FeedPostService : IFeedPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedPostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> AddFeedPost(FeedPost? feedPost)
        {
            if (feedPost is null) return false;
            return await _unitOfWork.FeedPostRepository.AddFeedPostAndSaveChanges(feedPost);
        }
    }
}