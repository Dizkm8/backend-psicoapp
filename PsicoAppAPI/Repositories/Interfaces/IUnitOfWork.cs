namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Get the IUserRepository
        /// </summary>
        /// <value>IUserRepository</value>
        public IUserRepository UserRepository { get; }
        /// <summary>
        /// Get the ISpecialistRepository
        /// </summary>
        /// <value>ISpecialistRepository</value>
        public ISpecialistRepository SpecialistRepository { get; }
    }
}