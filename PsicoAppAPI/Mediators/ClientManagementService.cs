using PsicoAppAPI.DTOs.Chat;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Models.Mobile;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class ClientManagementService : IClientManagementService
{
    private readonly IUserService _userService;
    private readonly ISpecialistService _specialistService;
    private readonly ISpecialistManagementService _specialistMediator;
    private readonly ITimeZoneService _timeZoneService;
    private readonly IUserManagementService _userMediator;
    private readonly IAuthManagementService _authMediator;
    private readonly IAppointmentService _appointmentService;
    private readonly IChatService _chatService;
    private readonly IOpenAiService _openAiService;
    private readonly IMapperService _mapperService;

    public ClientManagementService(IUserService userService, ISpecialistService specialistService,
        ISpecialistManagementService specialistManagementService, ITimeZoneService timeZoneService,
        IUserManagementService userMediator, IAuthManagementService authMediator,
        IAppointmentService appointmentService, IChatService chatService, IOpenAiService openAiService,
        IMapperService mapperService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
        _specialistMediator = specialistManagementService ??
                              throw new ArgumentNullException(nameof(specialistManagementService));
        _timeZoneService = timeZoneService ?? throw new ArgumentNullException(nameof(timeZoneService));
        _userMediator = userMediator ?? throw new ArgumentNullException(nameof(userMediator));
        _authMediator = authMediator ?? throw new ArgumentNullException(nameof(authMediator));
        _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
    }

    public async Task<bool> IsSpecialistAvailable(string specialistUserId, DateTime availability)
    {
        // Change utc 0 to utc-4 or utc-3 (Chile)
        var utcAvailability = await _timeZoneService.ConvertToChileUTC(availability);
        if (utcAvailability is null) return false;
        // If availability are not in specialist list return false
        var availabilities = await _specialistMediator.GetAvailabilitySlots(specialistUserId);
        if (availabilities is null) return false;

        var availabilityDto = availabilities
            .FirstOrDefault(x => x.StartTime == utcAvailability && x.IsAvailable);
        return availabilityDto is not null;
    }

    public async Task<bool> IsUserSpecialist(string userId)
    {
        var result = await _userMediator.IsUserSpecialist(userId);
        return result;
    }

    public async Task<bool> IsUserEnabled()
    {
        var result = await _authMediator.ExistsUserInTokenAndIsEnabled();
        return result;
    }

    public async Task<bool> AddAppointment(string specialistUserId, DateTime availability)
    {
        // Change utc 0 to utc-4 or utc-3 (Chile)
        var utcAvailability = await _timeZoneService.ConvertToChileUTC(availability);
        if (utcAvailability is null) return false;
        availability = (DateTime)utcAvailability;

        var clientUserId = _authMediator.GetUserIdInToken();
        if (clientUserId is null) return false;
        var appointmentResult = await _appointmentService.AddAppointment(clientUserId, specialistUserId, availability);
        if (!appointmentResult) return false;
        var disableAvailabilityResult = await _specialistService.DisableAvailability(specialistUserId, availability);
        return disableAvailabilityResult;
    }

    public async Task<SimpleMessageDto?> ChatWithBot(SimpleMessageDto sentMessage)
    {
        var query = sentMessage.Content;
        var response = await _openAiService.ChatWithGpt(query);
        if (response is null) return null;

        // Response is not null, so, we have to add 2 messages: query (from user) and response (from bot)
        // To chat history, to achieve this I prefer to call AddListOfMessage instead of
        // call twice the AddChatMessage method
        var userId = _authMediator.GetUserIdFromToken();
        if (userId is null) return null;
        // I create this message outside the list construction to use in the return, the userMessage
        // created inside the list will not be returned
        var botResponse = new ChatMessage
        {
            UserId = userId,
            Content = response,
            SendOn = DateTime.Now,
            IsBotAnswer = true
        };
        var messages = new List<ChatMessage>
        {
            // User message
            new ChatMessage
            {
                UserId = userId,
                Content = query,
                SendOn = DateTime.Now,
                IsBotAnswer = false
            },
            botResponse
        };
        var result = await _chatService.AddListOfChatMessages(messages);
        if (result is null) return null;
        return _mapperService.MapToSimpleMessageDto(botResponse);
    }
}