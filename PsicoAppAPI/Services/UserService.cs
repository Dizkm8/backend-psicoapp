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
        #region INJECTIONS
        readonly string _jwtSecret;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBCryptService _bCryptService;
        private readonly IMapperService _mapperService;
        #endregion

        #region CONSTANTS
        const string ADMIN_ROLE = "ADMIN";
        const string CLIENT_ROLE = "CLIENT";
        const string SPECIALIST_ROLE = "SPECIALIST";
        #endregion
        #endregion


        #region CLASS_METHODS
        public UserService(IConfiguration configuration, IUserRepository userRepository,
            IClientRepository clientRepository, ISpecialistRepository specialistRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, IBCryptService bCryptService
            , IMapperService mapperService)
        {
            _specialistRepository = specialistRepository ?? throw new ArgumentNullException(nameof(specialistRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            var token = configuration.GetValue<string>("JwtSettings:Secret") ??
                throw new ArgumentException("JwtSettings:Secret is null");
            _jwtSecret = token;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _bCryptService = bCryptService ?? throw new ArgumentNullException(nameof(bCryptService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        }

        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Token generated or null if the user doesn't exist</returns>
        private async Task<string?> GenerateJwtToken(string userId)
        {
            var user = _userRepository.GetUserById(userId).Result;
            if (user == null) return null;
            var userRole = await GetUserRole(userId);
            if (userRole is null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Get the role of the user based on their Id
        /// </summary>
        /// <returns>Role of the user, null if the user doesn't exists</returns>
        private async Task<string?> GetUserRole(string userId)
        {
            var client = await _clientRepository.GetClientById(userId);
            if (client != null) return client.IsAdministrator ? ADMIN_ROLE : CLIENT_ROLE;
            var specialist = await _specialistRepository.GetSpecialistById(userId);
            return specialist != null ? SPECIALIST_ROLE : null;
        }

        /// <summary>
        /// Get the user id from the token using HttpContext
        /// </summary>
        /// <returns>string with the Id. null if something gone wrong</returns>
        private string? GetUserIdInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser == null) return null;

            //Get Claims from JWT
            var userId = httpUser.FindFirstValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(userId) ? null : userId;
        }

        /// <summary>
        /// Get the user role from the token using HttpContext
        /// </summary>
        /// <returns>string with the Role. null if something gone wrong</returns>
        private string? GetUserRoleInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser == null) return null;

            //Get Claims from JWT
            var userRole = httpUser.FindFirstValue(ClaimTypes.Role);
            return string.IsNullOrEmpty(userRole) ? null : userRole;
        }

        private static User UpdateProfileInformationToUser(UpdateProfileInformationDto profileInformationDto, User user)
        {
            user.Name = profileInformationDto.Name;
            user.FirstLastName = profileInformationDto.FirstLastName;
            user.SecondLastName = profileInformationDto.SecondLastName;
            user.Email = profileInformationDto.Email;
            user.Gender = profileInformationDto.Gender;
            user.Phone = profileInformationDto.Phone;
            return user;
        }
        /// <summary>
        /// Get the userId from the token, found a user in Repository and return it
        /// </summary>
        /// <returns>
        /// User found. If the userId or Token are invalid 
        /// or simply user doesn't exists on repository return null
        ///</returns>
        private async Task<User?> GetUserUsingToken()
        {
            var userId = GetUserIdInToken();
            if (userId is null) return null;
            var user = await _userRepository.GetUserById(userId);
            return user is not null ? user : null;
        }
        #endregion


        #region IUSERSERVICE_METHODS
        public async Task<string?> GenerateToken(string? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User identifier cannot be null");
            }
            var token = await GenerateJwtToken(id);
            return token;
        }

        public async Task<User?> GetUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrWhiteSpace(loginUserDto.Id) || string.IsNullOrWhiteSpace(loginUserDto.Password)) return null;
            var user = await _userRepository.GetUserById(loginUserDto.Id);
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
            if(passwordHash is null) return null;
            // asigns to user
            user.Password = passwordHash;
            _ = await _userRepository.AddUserAndSaveChanges(user);
            // If user.Id is null its summons an empty string, RegisterClientDto it cannot be null
            // because of class itself with the controller validations,
            /// anyways, I made this to avoid warning message
            var client = _clientRepository.CreateClient(false, user.Id ?? "");
            _ = await _clientRepository.AddClientAndSaveChanges(client);
            return registerClientDto;
        }

        public async Task<User?> GetUserByEmail(string? email)
        {
            if (email == null) return null;
            var user = await _userRepository.GetUserByEmail(email);
            return user;
        }

        public async Task<bool> ExistsUserWithEmail(string? email)
        {
            if (email == null) return false;
            var result = await _userRepository.ExistsUserWithEmail(email);
            return result;
        }

        public async Task<bool> ExistsUserById(string? id)
        {
            if (id == null) return false;
            var result = await _userRepository.GetUserById(id) != null;
            return result;
        }

        public async Task<bool> ExistsUserByIdOrEmail(string? id, string? email)
        {
            if (id == null || email == null) return false;
            var result = await _userRepository.ExistsUserByIdOrEmail(id, email);
            return result;
        }

        public async Task<User?> GetUserByIdOrEmail(string? id, string? email)
        {
            if (id == null || email == null) return null;
            var result = await _userRepository.GetUserByIdOrEmail(id, email);
            return result;
        }

        public async Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser)
        {
            var userId = GetUserIdInToken();
            if (userId == null) return null;
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;

            var updateUser = UpdateProfileInformationToUser(newUser, user);
            var savedUser = _userRepository.UpdateUserAndSaveChanges(updateUser);
            var mappedDto = _mapper.Map<UpdateProfileInformationDto>(savedUser);
            return mappedDto;
        }

        public async Task<ProfileInformationDto?> GetUserProfileInformation()
        {
            var userId = GetUserIdInToken();
            var userRole = GetUserRoleInToken();
            if (userId == null || userRole == null) return null;

            var user = await _userRepository.GetUserById(userId);
            var profileInformationDto = _mapper.Map<ProfileInformationDto>(user);
            // Asign manually attribute cannot be mapped
            profileInformationDto.Role = userRole;

            return profileInformationDto;
        }

        public async Task<bool> ExistsEmailInOtherUser(string? email, string? id)
        {
            // GetUserByEmail also checks if email is null or empty
            // I do it anyways to avoid unnecessary calls
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(id)) return false;
            var user = await GetUserByEmail(email);
            if (user == null) return false;
            return user.Id != id;
        }

        public async Task<bool> ExistsEmailInOtherUser(string? email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            var user = await GetUserByEmail(email);
            if (user == null) return false;
            var userId = GetUserIdInToken();
            if (userId == null) return false;
            return user.Id != userId;
        }

        public async Task<bool> UpdateUserPassword(string? newPassword)
        {
            if(string.IsNullOrEmpty(newPassword)) return false;
            var user = await GetUserUsingToken();
            if(user is null) return false;
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password = passwordHash;
            var result = _userRepository.UpdateUserAndSaveChanges(user);
            return result is not null;
        }

        public async Task<bool> ExistsUserByToken()
        {
            var userId = GetUserIdInToken();
            if (userId is null) return false;
            var user = await _userRepository.GetUserById(userId);
            return user is not null;
        }

        public async Task<bool> CheckUsersPasswordUsingToken(string? password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            var user = await GetUserUsingToken();
            if(user is null) return false;
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
        #endregion
    }
}