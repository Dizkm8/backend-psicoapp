using PsicoAppAPI.DTOs.Chat;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IClientManagementService
{
    /// <summary>
    /// Check if a availability slot is available for the specialist with userId provided
    /// </summary>
    /// <param name="specialistUserId">user id of the specialist</param>
    /// <param name="availability">availability to check</param>
    /// <returns>True if it is available. false otherwise</returns>
    public Task<bool> IsSpecialistAvailable(string specialistUserId, DateTime availability);

    /// <summary>
    /// Check if the user with the Id provided is specialist and it is enabled
    /// </summary>
    /// <param name="userId">User id to check</param>
    /// <returns>true if it is specialist. otherwise false</returns>
    public Task<bool> IsUserSpecialist(string userId);

    /// <summary>
    /// Check if the user with the Id extracted from token
    /// </summary>
    /// <returns>true if it is enabled. otherwise false</returns>
    public Task<bool> IsUserEnabled();

    /// <summary>
    /// Add an appointment to the client (identified with userId from token)
    /// and specialist (identified with specialistUserId) in the provided Datetime availability
    /// The initial status of the appointment is Booked
    /// </summary>
    /// <returns>true if could be added. otherwise false</returns>
    public Task<bool> AddAppointment(string specialistUserId, DateTime availability);

    /// <summary>
    /// Send a message to GPT and get a response
    /// </summary>
    /// <param name="sentMessage">Content of the message</param>
    /// <returns>Message with response, null if something went wrong</returns>
    public Task<SimpleMessageDto?> ChatWithBot(SimpleMessageDto sentMessage);

    /// <summary>
    /// Get all the messages from User chat identified by their userId in token
    /// </summary>
    /// <returns>List with messagesDto. null if something went wrong</returns>
    public Task<List<MessageDto>?> GetChat();
}