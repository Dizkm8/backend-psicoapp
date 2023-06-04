using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly ISpecialistService _specialistService;

        public SpecialistManagementService(ISpecialistService specialistService)
        {
            _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
        }
    }
}