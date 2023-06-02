using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.ServiceMediators.Interfaces
{
    public interface IUserManagementService
    {
        /// <summary>
        /// Get user service
        /// </summary>
        /// <value>IUserService</value>
        public IUserService UserService { get; }
        /// <summary>
        /// Get auth service
        /// </summary>
        /// <value>IAuthService</value>
        public IAuthService AuthService { get; }
    }
}