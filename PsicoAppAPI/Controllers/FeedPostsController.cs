using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
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
            var validateContent = await _service.CheckPostContext(addFeedPost);
            if (!validateContent) return BadRequest(
                new ErrorModel { ErrorCode = 400, Message = "The Content don't follow the rules to post" });

            var existsTag = await _service.CheckPostTag(addFeedPost);
            if (!existsTag) return NotFound($"Tag with ID {addFeedPost.TagId} does not exist");

            var postToReturn = await _service.AddFeedPost(addFeedPost);
            if (postToReturn is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorModel { ErrorCode = 500, Message = "Internal error creating feedpost" });
            return Ok(postToReturn);
        }
    }
}