using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ForumPostsController : BaseApiController
{
    private readonly IForumPostManagementService _service;

    public ForumPostsController(IForumPostManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
}