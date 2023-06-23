using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services;

public class ForumPostService : IForumPostService
{
    private readonly IUnitOfWork _unitOfWork;

    public ForumPostService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> AddForumPost(ForumPost? post)
    {
        if (post is null) return false;
        return await _unitOfWork.ForumPostRepository.AddForumPostAndSaveChanges(post);
    }

    public async Task<List<ForumPost>> GetAllPosts()
    {
        var posts = await _unitOfWork.ForumPostRepository.GetAllPosts();
        return posts;
    }

    public async Task<bool> ExistsPost(int postId)
    {
        var result = await _unitOfWork.ForumPostRepository.ExistsPost(postId);
        return result;
    }

    public async Task<bool> AddComment(Comment comment)
    {
        var result = await _unitOfWork.CommentRepository.AddCommentAndSaveChanges(comment);
        return result;
    }
}