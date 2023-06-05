using PsicoAppAPI.DTOs.Specialist;

namespace PsicoAppAPI.Services.Mediators.Interfaces
{
    public interface ISpecialistManagementService
    {
        /// <summary>
        /// Get the availability slots of a specialist by its user id
        /// obtained from the token
        /// </summary>
        /// <param name="date">Initialdate to get the availability slots</param>
        /// <returns>List with the availabilities and if they're taken (IsAvailable bool)</returns>
        public Task<List<AvailabilitySlotDto>?> GetAvailabilitySlots(DateOnly date);
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
        /// Check if the date of the availabilities are in the allowed range between now and the next 8 weeks
        /// </summary>
        /// <param name="availabilities">IEnumerable with addAvialabilitiesDto</param>
        /// <returns>True if are valid, otherwise false</returns>
        public bool ValidateDateOfAvailabities(IEnumerable<AddAvailabilityDto> availabilities);
        /// <summary>
        /// Check if the time of the availabilities are in the allowed range between 8:00 and 20:00
        /// </summary>
        /// <param name="availabilities">IEnumerable with addAvialabilitiesDto</param>
        /// <returns>True if are valid, otherwise false</returns>
        public bool ValidateTimeOfAvailabities(IEnumerable<AddAvailabilityDto> availabilities);
    }
}