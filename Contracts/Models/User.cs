using System;


namespace Contracts.Models
{
    public abstract class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public string ImagePath { get; set; }
        public string RefreshToken { get; set; }
    }
}
