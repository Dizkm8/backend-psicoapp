using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExistsTagById(int id)
        {
            var tag = await _unitOfWork.TagRepository.GetTagById(id);
            return tag is not null;
        }

        public async Task<bool> ExistsTagByName(string? name)
        {
            if(string.IsNullOrEmpty(name)) return false;
            var tag = await _unitOfWork.TagRepository.GetTagByName(name);
            return tag is not null;
        }
    }
}