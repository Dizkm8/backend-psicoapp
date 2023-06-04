using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly ISpecialistService _specialistService;
        private readonly IAuthService _authService;
        private readonly IMapperService _mapperService;

        public SpecialistManagementService(ISpecialistService specialistService, IAuthService authService,
            IMapperService mapperService)
        {
            _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        }

        public async Task<List<AvailabilitySlotDto>?> GetAvailabilitySlots()
        {
            var userId = _authService.GetUserIdInToken();
            if (userId is null) return null;
            var availabilitySlots = await _specialistService.GetSpecialistAvailability(userId);
            if (availabilitySlots is null) return null;
            var mappedSlots = _mapperService.MapToListOfAvailabilitySlotDto(availabilitySlots);
            return mappedSlots;
        }
    }
}