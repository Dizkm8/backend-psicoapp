namespace PsicoAppAPI.Services.Interfaces
{
    public interface ISpecialistService
    {
        /// <summary>
        /// Get the availability of a specialist based on their userId
        /// </summary>
        /// <param name="userId">User id of the specialist</param>
        /// <returns>List with tuples, each tuple have the DateTime with the
        /// Availability slot created by specialist and if its available
        /// (the slot could be taked by other appointment)
        /// </returns>
        public Task<List<Tuple<DateTime, bool>>?> GetSpecialistAvailability(string userId);
        
    }
}