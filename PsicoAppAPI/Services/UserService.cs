using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Util;

namespace PsicoAppAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #region IUSERSERVICE_METHODS

        public async Task<User?> GetUserByCredentials(string userId, string password)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password)) return null;
            var user = await _unitOfWork.UserRepository.GetUserById(userId);
            if (user is null) return null;
            return BCryptHelper.VerifyPassword(password, user.Password) ? user : null;
        }

        private async Task<bool> AddUser(User user)
        {
            user.IsEnabled = true;
            // Hashes the password and check if it was successful
            var hashedPassword = BCryptHelper.HashPassword(user.Password);
            if (string.IsNullOrEmpty(hashedPassword)) return false;
            // assigns to user
            user.Password = hashedPassword;
            var result = await _unitOfWork.UserRepository.AddUserAndSaveChanges(user);
            return result;
        }

        public async Task<bool> AddSpecialist(User? user, int specialityId)
        {
            if (user is null) return false;
            user.RoleId = await GetIdOfSpecialistRole();
            var addUserResult = await AddUser(user);
            if (!addUserResult) return false;

            var specialist = new Specialist()
            {
                UserId = user.Id,
                SpecialityId = specialityId,
            };
            var addSpecialistResult = await _unitOfWork.SpecialistRepository.AddSpecialistAndSaveChanges(specialist);
            return addSpecialistResult;
        }

        public async Task<User?> GetUserByEmail(string? email)
        {
            if (email is null) return null;
            var user = await _unitOfWork.UserRepository.GetUserByEmail(email);
            return user;
        }

        public async Task<bool> ExistsUserWithEmail(string? email)
        {
            if (email is null) return false;
            var result = await _unitOfWork.UserRepository.ExistsUserWithEmail(email);
            return result;
        }

        public async Task<bool> ExistsUserById(string? id)
        {
            if (id is null) return false;
            var result = await _unitOfWork.UserRepository.GetUserById(id) is not null;
            return result;
        }

        public async Task<bool> ExistsUserByIdOrEmail(string? id, string? email)
        {
            if (id is null || email is null) return false;
            var result = await _unitOfWork.UserRepository.ExistsUserByIdOrEmail(id, email);
            return result;
        }

        public async Task<User?> GetUserByIdOrEmail(string? id, string? email)
        {
            if (id is null || email is null) return null;
            var result = await _unitOfWork.UserRepository.GetUserByIdOrEmail(id, email);
            return result;
        }

        public async Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser,
            string? userId, IMapperService mapperService)
        {
            if (userId is null) return null;
            var user = await _unitOfWork.UserRepository.GetUserById(userId);
            if (user is null) return null;
            var updateUser = mapperService.MapAttributesToUser(newUser, user);
            var savedUser = await _unitOfWork.UserRepository.UpdateUserAndSaveChanges(updateUser);
            var mappedDto = mapperService.MapToUpdatedProfileInformationDto(savedUser);
            return mappedDto;
        }

        public async Task<bool> ExistsEmailInOtherUser(string? email, string? id)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(id)) return false;
            var user = await GetUserByEmail(email);
            if (user is null) return false;
            return user.Id != id;
        }

        public async Task<User?> GetUserById(string? id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            var user = await _unitOfWork.UserRepository.GetUserById(id);
            return user;
        }

        public async Task<bool> UpdateUserPassword(string? userId, string? newPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(userId)) return false;
            var hashedPassword = BCryptHelper.HashPassword(newPassword);
            if (string.IsNullOrEmpty(hashedPassword)) return false;
            var user = await GetUserById(userId);
            if (user is null) return false;
            // Assign hashed password to user before save it
            user.Password = hashedPassword;
            var result = _unitOfWork.UserRepository.UpdateUserAndSaveChanges(user);
            return result is not null;
        }

        public async Task<int> GetIdOfClientRole()
        {
            var role = await _unitOfWork.RolesRepository.ClientRole();
            return role.Item1;
        }

        public async Task<int> GetIdOfAdminRole()
        {
            var role = await _unitOfWork.RolesRepository.AdminRole();
            return role.Item1;
        }

        public async Task<int> GetIdOfSpecialistRole()
        {
            var role = await _unitOfWork.RolesRepository.SpecialistRole();
            return role.Item1;
        }

        public async Task<int> GetRoleIdInUser(string? userId)
        {
            var user = await GetUserById(userId);
            if (user is null) return -1;
            return user.RoleId;
        }

        public async Task<bool> AddClient(User? user)
        {
            if (user is null) return false;
            user.RoleId = await GetIdOfClientRole();
            var result = await AddUser(user);
            return result;
        }

        public async Task<User?> UpdateUser(User? user)
        {
            if (user is null) return null;
            var updatedUser = await _unitOfWork.UserRepository.UpdateUserAndSaveChanges(user);
            return updatedUser;
        }

        public async Task<bool> CheckUpdateUser(User? user)
        {
            if (user is null) return false;
            var result = await _unitOfWork.UserRepository.UserCouldBeUpdatedUserAndSaveChanges(user);
            return result;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers();
            return users;
        }

        #endregion
    }
}