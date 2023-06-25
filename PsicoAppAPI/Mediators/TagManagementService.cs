using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators
{
    public class TagManagementService : ITagManagementService
    {
        private readonly ITagService _tagService;
        private readonly IMapperService _mapperService;

        public TagManagementService(ITagService tagService, IMapperService mapperService)
        {
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        }

        public async Task<IEnumerable<TagDto>> GetTags()
        {
            var tags = await _tagService.GetAllTags();
            var mappedTags = _mapperService.MapToTagDto(tags);
            return mappedTags;
        }
    }
}