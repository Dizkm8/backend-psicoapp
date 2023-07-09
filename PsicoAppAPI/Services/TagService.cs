using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> ExistsTagById(int id)
        {
            var result = await GetTagById(id);
            return result is not null;
        }

        public async Task<bool> ExistsTagByName(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var tag = await _unitOfWork.TagRepository.GetTagByName(name);
            return tag is not null;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            var tags = await _unitOfWork.TagRepository.GetTags();
            // If tags is null, return an empty list to avoid validations in the mediator
            return tags is null ? new List<Tag>() : tags;
        }

        public async Task<Tag?> GetTagById(int id)
        {
            var tag = await _unitOfWork.TagRepository.GetTagById(id);
            return tag;
        }
    }
}