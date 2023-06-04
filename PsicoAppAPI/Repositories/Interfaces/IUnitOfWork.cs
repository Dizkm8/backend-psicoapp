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
        /// <summary>
        /// Get the IRolesRepository
        /// </summary>
        /// <value>IRolesRepository</value>
        public IRolesRepository RolesRepository { get; }
        /// <summary>
        /// Get the IFeedPostRepository
        /// </summary>
        /// <value>IFeedPostRepository</value>
        public IFeedPostRepository FeedPostRepository { get; }
        /// <summary>
        /// Get the ITagRepository
        /// </summary>
        /// <value>ITagRepository</value>
        public ITagRepository TagRepository { get; }


    }
}