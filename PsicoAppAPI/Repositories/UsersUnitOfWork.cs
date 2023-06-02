namespace PsicoAppAPI.Repositories.Interfaces
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISpecialistRepository _specialistRepository;

        public UsersUnitOfWork(IUserRepository userRepository,
            IClientRepository clientRepository, ISpecialistRepository specialistRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _specialistRepository = specialistRepository ?? throw new ArgumentNullException(nameof(specialistRepository));
        }

        public IUserRepository UserRepository => _userRepository;

        public IClientRepository ClientRepository => _clientRepository;

        public ISpecialistRepository SpecialistRepository => _specialistRepository;
    }
}