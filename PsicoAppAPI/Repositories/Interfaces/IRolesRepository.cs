namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        /// <summary>
        /// Get the client role
        /// </summary>
        /// <returns>Tuple with <Id, Rol></returns>
        public Task<Tuple<int, string>> ClientRole();
        /// <summary>
        /// Get the admin role
        /// </summary>
        /// <returns>Tuple with <Id, Rol></returns>
        public Task<Tuple<int, string>> AdminRole();
        /// <summary>
        /// Get the specialist role
        /// </summary>
        /// <returns>Tuple with <Id, Rol></returns>
        public Task<Tuple<int, string>> SpecialistRole();
    }
}