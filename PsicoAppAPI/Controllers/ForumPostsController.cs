using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ForumPostsController : BaseApiController
{
    private readonly IForumPostManagementService _service;

    public ForumPostsController(IForumPostManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    // [Authorize(Roles = "3")]
    // [HttpPost("create-post")]
    // public async Task<ActionResult> AddFeedPost(AddFeedPostDto addFeedPost)
    // {
    //     var existsTag = await _service.CheckPostTag(addFeedPost);
    //     if (!existsTag) return NotFound($"Tag with ID {addFeedPost.TagId} does not exist");
    //
    //     var postToReturn = await _service.AddFeedPost(addFeedPost);
    //     if (postToReturn is null) return StatusCode(StatusCodes.Status500InternalServerError,
    //         new ErrorModel { ErrorCode = 500, Message = "Internal error creating feedpost" });
    //     return Ok(postToReturn);
    // }
}