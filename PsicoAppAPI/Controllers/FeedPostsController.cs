using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class FeedPostsController : BaseApiController
    {
        private readonly IFeedPostManagementService _service;

        public FeedPostsController(IFeedPostManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Authorize(Roles = "3")]
        [HttpPost("create-post")]
        public async Task<ActionResult> AddFeedPost(AddFeedPostDto addFeedPost)
        {
            var result = await _service.AddFeedPost(addFeedPost);
            if(!result) return BadRequest("Error creating post");
            return Ok(result);
        }
    }
}