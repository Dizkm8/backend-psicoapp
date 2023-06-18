using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ClientsController : BaseApiController
{
    private readonly IClientManagementService _service;

    public ClientsController(IClientManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [Authorize]
    [HttpPost("add-appointment/{specialistUserId}")]
    public async Task<ActionResult> GenerateAppointment(string specialistUserId,[FromQuery] DateTime dateTime)
    {
        return Ok();
    }
}
