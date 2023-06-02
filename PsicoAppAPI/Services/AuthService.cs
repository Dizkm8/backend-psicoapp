using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IMapperService _mapperService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, IUsersUnitOfWork usersUnitOfWork,
            IMapperService mapperService, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _usersUnitOfWork = usersUnitOfWork ?? throw new ArgumentNullException(nameof(usersUnitOfWork));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
    }
}