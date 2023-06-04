using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly ISpecialistService _specialistService;
        private readonly IAuthService _authService;

        public SpecialistManagementService(ISpecialistService specialistService, IAuthService authService)
        {
            _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        
    }
}