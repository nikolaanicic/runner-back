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
using System.Text;
using System.Threading.Tasks;

namespace Services.ClaimsService
{
    public class ClaimsManager : ServiceBase, IClaimManager
    {

        private IPasswordChecker _passwordChecker;
        private IConfiguration _configuration;

        public ClaimsManager(ILoggerManager logger, IRepositoryManager repository,IPasswordChecker checker,IConfiguration configuration) : base(logger, repository)
        {
            _passwordChecker = checker;
            _configuration = configuration;

        }

        public string GetCurrentUser(HttpContext currentContext)
        {
            return currentContext?.User?.Claims.First(c => c.Type == ClaimTypes.Name).Value ?? string.Empty;
        }

        public async Task<string> LogIn(PostUserLogInDto login)
        {
            var user = await _repository.Users.GetWithRole(login.Username, false);

            if (user == null)
                throw new NotFoundException($"User {login.Username} doesn't exist");
            else if (await _passwordChecker.CheckPassword(login.Password, user.PasswordHash) == false)
                throw new UnauthorizedException($"Invalid password");

            return CreateToken(user);
        }


        private string CreateToken(User user)
        {

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secret_key"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var tokenOptions = new JwtSecurityToken
                (
                issuer: _configuration["ValidIssuer"], 
                claims: CreateClaims(user.Username, user.Role.Rolename),
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["TokenValidityMinutes"])),
                signingCredentials:signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }

        private List<Claim> CreateClaims(string username,string userRole)
        {
            return new List<Claim> { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, userRole) };
        }
    }
}
