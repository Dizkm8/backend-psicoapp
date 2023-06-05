using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;
using PsicoAppAPI.Util;

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

        public async Task<IEnumerable<AvailabilitySlotDto>?> AddSpecialistAvailability(IEnumerable<AddAvailabilityDto> availabilities)
        {
            var userId = _authService.GetUserIdInToken();
            if (userId is null) return null;

            var mappedAvailabilities = _mapperService.MapToListOfAvailabilitySlot(availabilities, userId);
            if (mappedAvailabilities is null) return null;

            var result = await _specialistService.AddAvailabilities(mappedAvailabilities, userId);
            if (!result) return null;
            return _mapperService.MapToListOfAvailabilitySlotDto(mappedAvailabilities.ToList());
        }

        public async Task<List<AvailabilitySlotDto>?> GetAvailabilitySlots(DateOnly date)
        {
            var userId = _authService.GetUserIdInToken();
            if (userId is null) return null;
            // Get the initial date of the week to use as initial value of the range
            var startDate = DateHelper.GetMondayOfTheWeek(date);
            // Get the final date of the week to use as final value of the range
            var endDate = DateHelper.GetSundayOfTheWeek(date);

            var availabilitySlots = await _specialistService.GetAvailabilityByDate(userId, startDate, endDate);
            if (availabilitySlots is null) return null;
            var mappedSlots = _mapperService.MapToListOfAvailabilitySlotDto(availabilitySlots);
            return mappedSlots;
        }

        public bool ValidateDate(DateOnly date)
        {
            // Validate if the date is in the current week or greater && equals or less than 2 months (8 weeks)
            return DateHelper.DateIsOnWeekRange(date, 8);
        }

        public bool ValidateDateOfAvailabities(IEnumerable<AddAvailabilityDto> availabilities)
        {
            foreach (var availability in availabilities)
            {
                var date = DateOnly.FromDateTime(availability.StartTime);
                if (!DateHelper.DateIsBetweenNowAndSpecificWeek(date, 8)) return false;
            }
            return true;
        }
    }
}