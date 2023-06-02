namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IUsersUnitOfWork
    {
        /// <summary>
        /// Get user repository
        /// </summary>
        /// <value>IUserRepository</value>
        public IUserRepository UserRepository { get; }
        /// <summary>
        /// Get client repository
        /// </summary>
        /// <value>IClientRepository</value>
        public IClientRepository ClientRepository { get; }
        /// <summary>
        /// Get specialist repository
        /// </summary>
        /// <value>ISpecialistRepository</value>
        public ISpecialistRepository SpecialistRepository { get; }

    }
}