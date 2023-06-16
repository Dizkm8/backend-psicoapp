using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Util;

namespace PsicoAppAPI.Mediators
{
    public class SpecialistManagementService : ISpecialistManagementService
    {
        private readonly ISpecialistService _specialistService;
        private readonly IAuthService _authService;
        private readonly IMapperService _mapperService;
        private readonly ITimeZoneService _timeZoneService;

        public SpecialistManagementService(ISpecialistService specialistService, IAuthService authService,
            IMapperService mapperService, ITimeZoneService timeZoneService)
        {
            _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _timeZoneService = timeZoneService ?? throw new ArgumentNullException(nameof(timeZoneService));
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

        public async Task<bool> CheckDuplicatedAvailabilities(IEnumerable<AddAvailabilityDto> availabilities)
        {
            var userId = _authService.GetUserIdInToken();
            if (userId is null) return false;

            foreach (var availability in availabilities)
            {
                var startTime = availability.StartTime;
                var result = await _specialistService.ExistsAvailability(userId, startTime);
                if (result) return true;
            }
            // If none of the availabilities exists, return false
            return false;
        }

        public bool CheckHourRange(IEnumerable<AddAvailabilityDto> availabilities)
        {
            var result = availabilities.FirstOrDefault(x => x.StartTime.Hour < 8 || x.StartTime.Hour > 20);
            // if result is null means that all the availabilities are in the range
            return result is null;
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

        public async Task<IEnumerable<AddAvailabilityDto>?> TransformToChileUTC(IEnumerable<AddAvailabilityDto> availabilities)
        {
            try
            {
                availabilities = await Task.WhenAll(availabilities.Select(async x =>
                {
                    var dateTime = await _timeZoneService.ConvertToChileUTC(x.StartTime) ??
                        throw new Exception("Error converting to Chile UTC");
                    x.StartTime = dateTime;
                    return x;
                }));
            }
            catch (Exception)
            {
                return null;
            }
            return availabilities;
        }

        public bool ValidateDate(DateOnly date)
        {
            // Validate if the date is in the current week or greater && equals or less than 2 months (8 weeks)
            return DateHelper.DateIsOnWeekRange(date, 8);
        }
    }
}