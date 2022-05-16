using Contracts.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Entities.PasswordSecurity
{

    /// <summary>
    /// This class manages hashing and checking of the passwords for the whole application
    /// This implementation uses SHA256 algorithm without any salts 
    /// </summary>
    public class PasswordManager : IPasswordManager
    {
        public bool CheckPassword(string plaintextPassword, string hashedPassword)
        {
            return hashedPassword.Equals(Hash(plaintextPassword));
        }

        public string HashPassword(string password)
        {
            return Hash(password);
        }

        private string Hash(string plaintext)
        {
            using(var alg = SHA256.Create())
            {
                return Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plaintext)));
            }
        }
    }
}
