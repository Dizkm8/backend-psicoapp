using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
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