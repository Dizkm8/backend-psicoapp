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

        /// <summary>
        /// Get the IAvailabilitySlotRepository
        /// </summary>
        /// <value>IAvailabilitySlotRepository</value>
        public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }

        /// <summary>
        /// Get the IAppointmentRepository
        /// </summary>
        /// <value>IAppointmentRepository</value>
        public IAppointmentRepository AppointmentRepository { get; }

        /// <summary>
        /// Get the IAppointmentStatusesRepository
        /// </summary>
        /// <value>IAppointmentStatusesRepository</value>
        public IAppointmentStatusesRepository AppointmentStatusesRepository { get; }

        /// <summary>
        /// Get the IForumPostRepository
        /// </summary>
        /// <value>IForumPostRepository</value>
        public IForumPostRepository ForumPostRepository { get; }
        /// <summary>
        /// Get the IGptRulesRepository
        /// </summary>
        /// <value>IGptRulesRepository</value>
        public IGptRulesRepository GptRulesRepository { get; }
    }
}