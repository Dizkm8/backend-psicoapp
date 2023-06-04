using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface ISpecialistService
    {
        /// <summary>
        /// Get the availability of a specialist based on their userId
        /// </summary>
        /// <param name="userId">User id of the specialist</param>
        /// <returns>List with the availables slots of the specialist.
        /// Null if the specialist dont have availability
        /// </returns>
        public Task<List<AvailabilitySlot>?> GetSpecialistAvailability(string? userId);

    }
}