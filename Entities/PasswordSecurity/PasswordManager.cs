using Contracts.Security.Passwords;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entities.PasswordSecurity
{

    /// <summary>
    /// This class manages hashing and checking of the passwords for the whole application
    /// This implementation uses SHA256 algorithm without any salts 
    /// </summary>
    public class PasswordManager : IPasswordManager
    {
        public async Task<bool> CheckPassword(string plaintextPassword, string hashedPassword)
        {
            return hashedPassword.Equals(await HashAsync(plaintextPassword));
        }

        public string HashPassword(string password)
        {
            return Hash(password);
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            return await HashAsync(password);
        }

        private async Task<string> HashAsync(string plaintext)
        {
            using(var alg = SHA256.Create())
            {
                return Convert.ToBase64String(await alg.ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(plaintext))));
            }
        }

        private string Hash(string plaintext)
        {
            using (var alg = SHA256.Create())
            {
                return Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plaintext)));
            }
        }
    }
}
