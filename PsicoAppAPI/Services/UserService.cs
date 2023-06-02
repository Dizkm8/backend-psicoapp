using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class UserService : IUserService
    {
        #region CLASS_ATTRIBUTES
        private readonly IMapper _mapper;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IBCryptService _bCryptService;
        private readonly IMapperService _mapperService;
        #endregion


        #region CLASS_METHODS
        public UserService(IUsersUnitOfWork usersUnitOfWork, IMapper mapper,
            IBCryptService bCryptService, IMapperService mapperService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bCryptService = bCryptService ?? throw new ArgumentNullException(nameof(bCryptService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _usersUnitOfWork = usersUnitOfWork ?? throw new ArgumentNullException(nameof(usersUnitOfWork));
        }
        #endregion


        #region IUSERSERVICE_METHODS
        public async Task<User?> GetUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrWhiteSpace(loginUserDto.Id) || string.IsNullOrWhiteSpace(loginUserDto.Password)) return null;
            var user = await _usersUnitOfWork.UserRepository.GetUserById(loginUserDto.Id);
            if (user is null) return null;
            return BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password) ? user : null;
        }

        public async Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto)
        {
            var user = _mapperService.MapToUser(registerClientDto);
            if (user == null) return null;
            user.IsEnabled = true;
            // Hashes the password and check if it was successful
            var passwordHash = _bCryptService.HashPassword(registerClientDto.Password);
            if (passwordHash is null) return null;
            // asigns to user
            user.Password = passwordHash;
            _ = await _usersUnitOfWork.UserRepository.AddUserAndSaveChanges(user);
            // If user.Id is null its summons an empty string, RegisterClientDto it cannot be null
            // because of class itself with the controller validations,
            /// anyways, I made this to avoid warning message
            var client = _usersUnitOfWork.ClientRepository.CreateClient(false, user.Id ?? "");
            _ = await _usersUnitOfWork.ClientRepository.AddClientAndSaveChanges(client);
            return registerClientDto;
        }

        public async Task<User?> GetUserByEmail(string? email)
        {
            if (email == null) return null;
            var user = await _usersUnitOfWork.UserRepository.GetUserByEmail(email);
            return user;
        }

        public async Task<bool> ExistsUserWithEmail(string? email)
        {
            if (email == null) return false;
            var result = await _usersUnitOfWork.UserRepository.ExistsUserWithEmail(email);
            return result;
        }

        public async Task<bool> ExistsUserById(string? id)
        {
            if (id == null) return false;
            var result = await _usersUnitOfWork.UserRepository.GetUserById(id) != null;
            return result;
        }

        public async Task<bool> ExistsUserByIdOrEmail(string? id, string? email)
        {
            if (id == null || email == null) return false;
            var result = await _usersUnitOfWork.UserRepository.ExistsUserByIdOrEmail(id, email);
            return result;
        }

        public async Task<User?> GetUserByIdOrEmail(string? id, string? email)
        {
            if (id == null || email == null) return null;
            var result = await _usersUnitOfWork.UserRepository.GetUserByIdOrEmail(id, email);
            return result;
        }

        public async Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser, string? userId)
        {
            if (userId == null) return null;
            var user = await _usersUnitOfWork.UserRepository.GetUserById(userId);
            if (user == null) return null;
            var updateUser = _mapperService.MapAttributesToUser(newUser, user);
            var savedUser = _usersUnitOfWork.UserRepository.UpdateUserAndSaveChanges(updateUser);
            var mappedDto = _mapper.Map<UpdateProfileInformationDto>(savedUser);
            return mappedDto;
        }

        public async Task<ProfileInformationDto?> GetUserProfileInformation(string? userId, string? userRole)
        {
            if (userId == null || userRole == null) return null;
            var user = await _usersUnitOfWork.UserRepository.GetUserById(userId);
            var profileInformationDto = _mapper.Map<ProfileInformationDto>(user);
            // Asign manually attribute cannot be mapped
            profileInformationDto.Role = userRole;

            return profileInformationDto;
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
            var user = await _usersUnitOfWork.UserRepository.GetUserById(id);
            return user;
        }

        public async Task<bool> UpdateUserPassword(string? userId, string? hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(userId)) return false;
            var user = await GetUserById(userId);
            if (user is null) return false;
            // Assign hashed password to user before save it
            user.Password = hashedPassword;
            var result = _usersUnitOfWork.UserRepository.UpdateUserAndSaveChanges(user);
            return result is not null;
        }
        #endregion
    }
}