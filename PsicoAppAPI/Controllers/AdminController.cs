using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class AdminController : BaseApiController
{
    private readonly IAdminManagementService _service;

    public AdminController(IAdminManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public async Task<ActionResult<string>> GetGptRules()
    {
        var isAdmin = await _service.IsUserAdmin();
        if (!isAdmin) return Unauthorized("The user with userId from token are not a valid admin");

        var rule = await _service.GetModerationRules();
        if (rule is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error getting moderation rules" });
        return Ok(rule);
    }
}