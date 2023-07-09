using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.BasePosts;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class TagsController : BaseApiController
    {
        private readonly ITagManagementService _service;

        public TagsController(ITagManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Get all tags in the system shaped as TagDto
        /// </summary>
        /// <returns>
        /// Returns a list of TagDto, each tagDto have the following properties:
        /// - Id: int
        /// - Name: string
        /// If the system have no tags, returns an empty list
        /// If the user is not logged in, returns a 401 status: Unauthorized
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<TagDto>> GetTags()
        {
            var tags = await _service.GetTags();
            return tags;
        }
    }
}