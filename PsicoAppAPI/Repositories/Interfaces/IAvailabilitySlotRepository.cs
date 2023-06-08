using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IAvailabilitySlotRepository
    {
        /// <summary>
        /// Get the list of availability slots of a specialist based on their userId
        /// </summary>
        /// <param name="userId">Specialist user Id</param>
        /// <returns>List with the availability slots. Null if the userId don't have</returns>
        public Task<List<AvailabilitySlot>?> GetAvailabilitySlotsByUserId(string userId);
        /// <summary>
        /// Get the list of availability slots of a specialist based on their userId and the range of dates
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="startDate">Start date to get</param>
        /// <param name="endDate">End date to get</param>
        /// <returns></returns>
        public Task<List<AvailabilitySlot>?> GetAvailabiliySlotByUserIdAndDateRange(string userId , DateOnly startDate, DateOnly endDate);
        /// <summary>
        /// Add new availabilities to a specialist
        /// </summary>
        /// <param name="availabilities">IEnumerable for availabilities</param>
        /// <param name="userId">User id</param>
        /// <returns>True if could be added, otherwise false</returns>
        public Task<bool> AddAvailabilitiesToUser(IEnumerable<AvailabilitySlot> availabilities, string userId);
    }
}