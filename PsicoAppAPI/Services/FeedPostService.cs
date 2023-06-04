using PsicoAppAPI.DTOs.FeedPost;
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
        
        public Task<bool> AddFeedPost(FeedPost? feedPost)
        {
            throw new NotImplementedException();
        }
    }
}