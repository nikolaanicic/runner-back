﻿using Microsoft.AspNetCore.Http;
using System;

namespace Contracts.Dtos
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IFormFile Image { get; set; }
    }
}
