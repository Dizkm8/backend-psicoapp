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
        /// Sets to disabled a specific availability of a specialists identified by their userId
        /// </summary>
        /// <param name="userId">User id of the specialist</param>
        /// <param name="availabilityDate">Date of their availability</param>
        /// <returns>true if could be updated. otherwise false</returns>
        public Task<bool> DisableAvailability(string userId, DateTime availabilityDate);
        /// <summary>
        /// Check if a speciality exists by their Id
        /// </summary>
        /// <param name="specialityId">Id of the speciality</param>
        /// <returns>true if exists. otherwise false</returns>
        public Task<bool> CheckSpecialityById(int specialityId);
        /// <summary>
        /// Get a speciality by their Id
        /// </summary>
        /// <param name="specialityId">Id of the speciality</param>
        /// <returns>Speciality if could be found. otherwise null</returns>
        public Task<Speciality?> GetSpecialityById(int specialityId);
        /// <summary>
        /// Get all the specialities of the system
        /// </summary>
        /// <returns>List with Speciality</returns>
        public Task<List<Speciality>> GetAllSpecialities();
    }
}