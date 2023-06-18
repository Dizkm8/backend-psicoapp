using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ClientController : BaseApiController
{
    private readonly IClientManagementService _service;
    
    public ClientController(IClientManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
}
