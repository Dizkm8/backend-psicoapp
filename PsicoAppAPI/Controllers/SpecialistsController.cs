using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class SpecialistsController : BaseApiController
    {
        private ISpecialistManagementService _service;

        public SpecialistsController(ISpecialistManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}