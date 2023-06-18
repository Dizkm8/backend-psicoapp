using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class ClientManagementService : IClientManagementService
{
    private readonly IUserService _userService;
    private readonly ISpecialistService _specialistService;
    private readonly ISpecialistManagementService _specialistMediator;
    private readonly ITimeZoneService _timeZoneService;

    public ClientManagementService(IUserService userService, ISpecialistService specialistService,
        ISpecialistManagementService specialistManagementService, ITimeZoneService timeZoneService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
        _specialistMediator = specialistManagementService ??
                              throw new ArgumentNullException(nameof(specialistManagementService));
        _timeZoneService = timeZoneService ?? throw new ArgumentNullException(nameof(timeZoneService));
    }

    public async Task<bool> IsSpecialistAvailable(string specialistUserId, DateTime availability)
    {
        // Change utc 0 to utc-4 or utc-3 (Chile)
        var utcAvailability = await _timeZoneService.ConvertToChileUTC(availability);
        if(utcAvailability is null) return false;
        // If availability are not in specialist list return false
        var availabilities = await _specialistMediator.GetAvailabilitySlots(specialistUserId);
        if(availabilities is null) return false;

        var availabilityDto = availabilities
            .FirstOrDefault(x => x.StartTime == utcAvailability && x.IsAvailable);
        return availabilityDto is not null;
    }
}
