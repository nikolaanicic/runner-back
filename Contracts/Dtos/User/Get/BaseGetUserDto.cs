using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.User.Get
{
    public abstract class BaseGetUserDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string ImagePath { get; set; }
    }
}
