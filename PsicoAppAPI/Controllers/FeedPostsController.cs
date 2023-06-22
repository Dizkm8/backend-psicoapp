using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class FeedPostsController : BaseApiController
    {
        private readonly IFeedPostManagementService _service;

        public FeedPostsController(IFeedPostManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Create a new Feed post
        /// </summary>
        /// <param name="addFeedPost">
        /// Title: Post's title, must be not null or empty
        /// Content: Post's content, must be not null or empty and less than 255 characters
        /// TagId: Post's tag id, must be not null and exist in the database
        /// </param>
        /// <returns>
        /// If the User Id from the provided token does not exists or is disabled
        /// If the user Id from the provided token doesn't match with a client return 500 Internal server error
        /// If something went wrong adding the post return 500 Internal server error
        /// If the tag id does not exist return 404 Not found
        [Authorize(Roles = "3")]
        [HttpPost("create-post")]
        public async Task<ActionResult> AddFeedPost(AddFeedPostDto addFeedPost)
        {
            var existsTag = await _service.CheckPostTag(addFeedPost);
            if (!existsTag) return NotFound($"Tag with ID {addFeedPost.TagId} does not exist");

            var postToReturn = await _service.AddFeedPost(addFeedPost);
            if (postToReturn is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorModel { ErrorCode = 500, Message = "Internal error creating feedpost" });
            return Ok(postToReturn);
        }
    }
}