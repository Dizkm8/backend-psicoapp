using PsicoAppAPI.DTOs;
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
        public Task<List<AvailabilitySlot>?> GetAllAvailability(string? userId);
        /// <summary>
        /// Get the availability of a specialist based on their userId and the range of dates
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="StartDate">Mininum date of availability</param>
        /// <param name="EndDate">Maximum date of availability</param>
        /// <returns></returns>
        public Task<List<AvailabilitySlot>?> GetAvailabilityByDate(string? userId, DateOnly StartDate, DateOnly EndDate);
        /// <summary>
        /// Add a new availability to a specialist
        /// </summary>
        /// <param name="availabilities">List of availabilities</param>
        /// <param name="userId">user Id</param>
        /// <returns>True if could be added, otherwise false</returns>
        public Task<bool> AddAvailabilities(IEnumerable<AvailabilitySlot> availabilities, string userId);
        /// <summary>
        /// Check if exists an availability of a specialist based on their userId and the Hour field of startTime
        /// </summary>
        /// <param name="userId">Specialist user Id</param>
        /// <param name="startTime">Starttime of the availability</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsAvailability(string userId, DateTime startTime);
        /// <summary>
        /// Get all the specialist which match with the users in the provided array
        /// </summary>
        /// <param name="users">Users to map</param>
        /// <returns>Enumerable with Specialists Dto</returns>
        public Task<IEnumerable<SpecialistDto>> GetSpecialistsByUserList(IEnumerable<User> users);
    }
}