using PsicoAppAPI.DTOs.Specialist;

namespace PsicoAppAPI.Mediators.Interfaces
{
    public interface ISpecialistManagementService
    {
        /// <summary>
        /// Get the availability slots of a specialist by its user id
        /// obtained from the token
        /// </summary>
        /// <param name="date">Initialdate to get the availability slots</param>
        /// <returns>List with the availabilities and if they're taken (IsAvailable bool)</returns>
        public Task<List<AvailabilitySlotDto>?> GetAvailabilitySlots(string userId);
        /// <summary>
        /// Validates if the date is in 8 weeks range from the current week
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <returns>True if its valid, otherwise false</returns>
        public bool ValidateDate(DateOnly date);
        /// <summary>
        /// Add a new availabilities to an specialist in the database
        /// </summary>
        /// <param name="availabilities">Availabilities to add</param>
        /// <returns>True if could be added, otherwise false</returns>
        public Task<IEnumerable<AvailabilitySlotDto>?> AddSpecialistAvailability(IEnumerable<AddAvailabilityDto> availabilities);
        /// <summary>
        /// Check if the availabilities provided exists in the database
        /// </summary>
        /// <param name="availabilities">Availabilities to validate</param>
        /// <returns>True if are duplicated, otherwise false.</returns>
        public Task<bool> CheckDuplicatedAvailabilities(IEnumerable<AddAvailabilityDto> availabilities);
        /// <summary>
        /// Transform the availabilities Startime to Chilean UTC
        /// </summary>
        /// <param name="availabilities">Availabilities to update</param>
        /// <returns>IEnumerable with availabilities updates, null if cannot be converted</returns>
        public Task<IEnumerable<AddAvailabilityDto>?> TransformToChileUTC(IEnumerable<AddAvailabilityDto> availabilities);
        /// <summary>
        /// Check if the hour range of the availabilities are valid
        /// </summary>
        /// <param name="availabilities">Availabilities to check</param>
        /// <returns>True if they're valid. null otherwise</returns>
        public bool CheckHourRange(IEnumerable<AddAvailabilityDto> availabilities);
        
        
    }
}