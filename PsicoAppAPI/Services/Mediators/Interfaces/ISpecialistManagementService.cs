using PsicoAppAPI.DTOs.Specialist;

namespace PsicoAppAPI.Services.Mediators.Interfaces
{
    public interface ISpecialistManagementService
    {
        /// <summary>
        /// Get the availability slots of a specialist by its user id
        /// obtained from the token
        /// </summary>
        /// <returns>List with the availabilities and if they're taken (IsAvailable bool)</returns>
        public Task<List<AvailabilitySlotDto>?> GetAvailabilitySlots();
    }
}