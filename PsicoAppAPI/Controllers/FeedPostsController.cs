using System.ComponentModel.DataAnnotations;
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
        /// </returns>
        [Authorize(Roles = "3")]
        [HttpPost("create-post")]
        public async Task<ActionResult> AddFeedPost(AddFeedPostDto addFeedPost)
        {
            var existsTag = await _service.CheckPostTag(addFeedPost);
            if (!existsTag) return NotFound($"Tag with ID {addFeedPost.TagId} does not exist");

            var postToReturn = await _service.AddFeedPost(addFeedPost);
            if (postToReturn is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error creating feedpost" });
            return Ok(postToReturn);
        }

        /// <summary>
        /// Get all feed posts
        /// </summary>
        /// <returns>
        /// If the user Id from the provided token doesn't match with a client return 500 Internal server error
        /// If something went wrong fetching the posts return 500 Internal server error
        /// If everything goes well, return a List with the following properties:
        /// Id: Post's id
        /// Title: Post's title
        /// Content: Post's content
        /// PublishedOn: Post's published date (ISO 8601)
        /// UserId: Post's user id
        /// 
        /// The next three attributes are used to show the user's full name
        /// I suggest threat like "private" stuff, so they are not used in the client side
        /// use fullName attribute instead
        /// UserName: Post's user name 
        /// UserFirstLastName: Post's user first last name
        /// UserSecondLastName: Post's user second last name
        /// 
        /// FullName: Post's user full name
        /// TagName: Post's tag name
        /// </returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedPostDto>>> GetAllForumPosts()
        {
            var posts = await _service.GetAllPosts();
            if (posts is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error fetching all forum posts" });
            return Ok(posts);
        }

        /// <summary>
        /// Delete a post by their post Id
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>
        /// If the user Id from the provided token doesn't match with a admin return a status 401 Unauthorized with a custom message
        /// If the the post Id do not match with any post return a status404 with a BadRequest with custom message
        /// If something went wrong deleting the post return a status 500 internal server error with a custom messsage
        /// If everything goes well return a status 200
        /// </returns>
        [Authorize(Roles = "1")]
        [HttpDelete("delete-post/{postId:int}")]
        public async Task<ActionResult> DeletePost([Required] int postId)
        {
            var isSpecialist = await _service.IsUserAdmin();
            if (!isSpecialist) return Unauthorized("The user with userId from token are not a valid admin");

            var existsPost = await _service.ExistsPost(postId);
            if (!existsPost) return BadRequest("Post Id do not match with any existing post");

            var result = await _service.DeletePost(postId);
            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error deleting the post" });
            return Ok();
        }
    }
}