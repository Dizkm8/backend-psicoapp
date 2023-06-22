using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ForumPostsController : BaseApiController
{
    private readonly IForumPostManagementService _service;

    public ForumPostsController(IForumPostManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [Authorize(Roles = "2")]
    [HttpPost("create-post")]
    public async Task<ActionResult> AddForumPost(AddForumPostDto addForumPost)
    {
        var existsTag = await _service.CheckPostTag(addForumPost);
        if (!existsTag) return NotFound($"Tag with ID {addForumPost.TagId} does not exist");
    
        var postToReturn = await _service.AddForumPost(addForumPost);
        if (postToReturn is null) return StatusCode(StatusCodes.Status500InternalServerError,
            new ErrorModel { ErrorCode = 500, Message = "Internal error creating feedpost" });
        return Ok(postToReturn);
    }
}