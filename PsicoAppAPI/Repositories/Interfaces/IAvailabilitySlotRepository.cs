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
    }
}