using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class TagManagementService : ITagManagementService
    {
        private readonly ITagService _tagService;

        public TagManagementService(ITagService tagService)
        {
            _tagService = tagService;
        }

        public Task<IEnumerable<TagDto>> GetTags()
        {
            // var result = await _tagService.GetTags();
            throw new NotImplementedException();
        }
    }
}