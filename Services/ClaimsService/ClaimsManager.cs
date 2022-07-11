using Contracts.Dtos.Login;
using Contracts.Dtos.User.Post;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Security.Claims;
using Contracts.Security.Passwords;
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

        public ClaimsManager(ILoggerManager logger, IRepositoryManager repository,IPasswordChecker checker,IConfiguration configuration) : base(logger, repository)
        {
            _passwordChecker = checker;
            _configuration = configuration;

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


        /// <summary>
        /// This method logs users in i.e. it returns the token if the user exists and the password is correct
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> LogIn(PostUserLogInDto login)
        {
            var user = await _repository.Users.GetWithRole(login.Username, true);

            if (user == null)
                throw new NotFoundException($"User {login.Username} doesn't exist");
            else if (await _passwordChecker.CheckPassword(login.Password, user.PasswordHash) == false)
                throw new UnauthorizedException($"Invalid password");

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
                claims: CreateClaims(user.Username, user.Role.Rolename,user.Id),
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
        private List<Claim> CreateClaims(string username, string userRole,long id )
        {
            if(userRole == RolesConstants.Admin || userRole == RolesConstants.Deliverer)
                return new List<Claim> { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, userRole)};
            return new List<Claim> { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, userRole), new Claim(ClaimTypes.NameIdentifier,id.ToString())};

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
            string username = string.Empty, role = string.Empty;

            try
            {
                username = claims[0].Value;
                role = claims[1].Value;
            }catch
            {
                throw new SecurityException("Invalid or malformed refresh token");
            }

            var user = await _repository.Users.GetWithRole(username, true);

            if(user == null || user.Role.Rolename != role || user.RefreshToken != refreshDto.RefreshToken)
            {
                throw new SecurityException("Invalid or malformed refresh token");
            }

            string newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _repository.SaveAsync();

            return new LoginResponseDto { Token = CreateToken(user), RefreshToken = newRefreshToken, Role = role };


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
    }
}
