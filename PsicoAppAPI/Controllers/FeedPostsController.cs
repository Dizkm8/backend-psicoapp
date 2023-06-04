using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class FeedPostsController : BaseApiController
    {
        private readonly IFeedPostService _service;

        public FeedPostsController(IFeedPostService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Authorize(Roles = "3")]
        [HttpPost("create-post")]
        public async Task<ActionResult> AddFeedPost(AddFeedPostDto addFeedPost)
        {

            return Ok();
        }
    }
}