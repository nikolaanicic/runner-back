using Contracts.Dtos.Login;
using Contracts.Dtos.User.Post;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Security.Claims;
using Contracts.Security.Passwords;
using Contracts.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClaimsService
{

    /// <summary>
    /// This clas manages user claims
    /// </summary>
    public class ClaimsManager : ServiceBase, IClaimManager
    {

        private IPasswordChecker _passwordChecker;
        private IConfiguration _configuration;
        private IUserService _userManager;

        public ClaimsManager(ILoggerManager logger, IRepositoryManager repository,IPasswordChecker checker,IConfiguration configuration,IUserService userManager) : base(logger, repository)
        {
            _passwordChecker = checker;
            _configuration = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// This method gets the username from the HttpContext
        /// The username is embedded in user claims with the ClaimType.Name
        /// Here we just get the claim with that type and the value associated with it
        /// If there is no value the method returns an empty string
        /// </summary>
        /// <param name="currentContext"></param>
        /// <returns></returns>
        public string GetCurrentUser(HttpContext currentContext)
        {
            return currentContext?.User?.Claims.First(c => c.Type == ClaimTypes.Name).Value ?? string.Empty;
        }

        public string GetCurrentEmail(HttpContext currentContext)
        {
            return currentContext?.User?.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? string.Empty;
        }



        /// <summary>
        /// This method logs users in i.e. it returns the token if the user exists and the password is correct
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> LogIn(PostUserLogInDto login)
        {
            var user = await _repository.Users.GetWithRoleByEmailAsync(login.Email, true);

            if (user == null)
                throw new NotFoundException($"User {login.Email} doesn't exist");
            else if (await _passwordChecker.CheckPassword(login.Password, user.PasswordHash) == false)
                throw new UnauthorizedException($"Invalid password");
            else if (user.Role.Rolename == RolesConstants.Deliverer && (user as Deliverer).State == ProfileState.DENIED)
                throw new UnauthorizedException($"Your registration request has been denied");
            else if (user.Role.Rolename == RolesConstants.Deliverer && (user as Deliverer).State == ProfileState.PROCESSING)
                throw new UnauthorizedException($"Your registration request has not been approved yet");


            string refreshToken = CreateRefreshToken();
            user.RefreshToken = refreshToken;
            await _repository.SaveAsync();

            return new LoginResponseDto { Token = CreateToken(user), Role = user.Role.Rolename,RefreshToken = refreshToken };
        }


        /// <summary>
        /// This method actually creates the token
        /// It configures jwtSecurityOptions, adds the valid issuer, users claims, expiration and signing credentials
        /// secret_key is dotnet user_secret that is loaded in the configuration file in runtime
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(User user)
        {

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secret_key"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var tokenOptions = new JwtSecurityToken
                (
                issuer: _configuration["ValidIssuer"], 
                claims: CreateClaims(user.Username, user.Role.Rolename,user.Id,user.Email),
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["TokenValidityMinutes"])),
                signingCredentials:signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }

        /// <summary>
        /// This method creates a refresh token which the logged in user 
        /// will use to renew his access to the application
        /// </summary>
        /// <returns></returns>
        private string CreateRefreshToken()
        {
            var rand = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(rand);
            }
            return Convert.ToBase64String(rand);
        }


        /// <summary>
        /// This method creates the claims list
        /// Claims list contains users role and users username
        /// Username is added with ClaimType.Name 
        /// Role is added with ClaimType.Role
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        private List<Claim> CreateClaims(string username, string userRole,long id,string email)
        {
            var claims = new List<Claim>{ new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, userRole),new Claim(ClaimTypes.Email,email)};
            if (userRole == RolesConstants.Consumer)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
            }
            return claims;
        }


        /// <summary>
        /// This method checks the validity of the expired token
        /// Gets the username and role from the expired token
        /// Checks the validity of the refresh token
        /// If everything is valid it returns a new token and a new refresh token
        /// </summary>
        /// <param name="refreshDto"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> RefreshToken(RefreshTokenPostDto refreshDto)
        {
            var claims = this.GetPrincipalFromExpiredToken(refreshDto.ExpiredToken).Claims.ToList();
            string email = string.Empty, role = string.Empty;

            try
            {
                email = claims[2].Value;
                role = claims[1].Value;
            }catch
            {
                throw new SecurityException("Invalid or malformed refresh token");
            }

            var user = await _repository.Users.GetWithRoleByEmailAsync(email, true);

            if(user == null || user.Role.Rolename != role || user.RefreshToken != refreshDto.RefreshToken)
            {
                throw new SecurityException("Invalid or malformed refresh token");
            }

            string newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _repository.SaveAsync();

            return CreateResponse(user);


        }

        private LoginResponseDto CreateResponse(User user)
        {
            return new LoginResponseDto { Token = CreateToken(user), RefreshToken = user.RefreshToken, Role = user.Role.Rolename };
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secret_key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token;
            var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParams, out token);

            var jwtSecurityToken = token as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");


            return principal;
        }

        public async Task<LoginResponseDto> CreateGoogleUser(GoogleLoginDto dto)
        {
            var payload = await VerifyGoogleToken(dto);
            if (payload == null)
                throw new BadRequestException("Invalid google authentication token");

            var email = payload.Email;
            var user = await _repository.Users.GetWithRoleByEmailAsync(email, false);

            if (user != null)
                return CreateResponse(user);

            string username = string.Empty;

            do
            {
                username = GenerateRandomUsername();

            } while ((await _repository.Users.GetByUsernameAsync(username, false) != null));

            var newUserDto = ParsePayloadToDto(payload);
            newUserDto.Email = email;
            newUserDto.Username = username;

            await _userManager.Register(newUserDto);

            user.RefreshToken = CreateRefreshToken();
            await _repository.SaveAsync();

            return CreateResponse(user);

        }


        private PostUserDto ParsePayloadToDto(GoogleJsonWebSignature.Payload payload)
        {
            var password = GenerateRandomPassword();
            var name = payload.GivenName;
            var lastName = payload.FamilyName;
            var rolename = RolesConstants.Consumer;
            var birthdate = new DateTime(1999, 7, 12);
            var address = "15302 Korenita";

            var newUserDto = new PostUserDto
            {
                Address = address,
                DateOfBirth = birthdate,
                Image = null,
                LastName = lastName,
                Name = name,
                Password = password,
                RoleName = rolename,
            };

            return newUserDto;
        }

        private string GenerateRandomString()
        {
            var uname = CreateRefreshToken();
            if (uname.Length > 8)
                return uname.Substring(0, 8);
            return uname;
        }


        private string GenerateRandomUsername()
        {
            return GenerateRandomString();
        }

        private string GenerateRandomPassword()
        {
            return GenerateRandomString() + "12";
        }



        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginDto dto)
        {
            try
            {
                var googleAuth = _configuration.GetSection("Authentication:Google");

                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { googleAuth["ClientId"] }

                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);
                return payload;
            }catch
            {
                return null;
            }
        }
    }
}
