using Contracts.Dtos.User.Post.Validators;
using Contracts.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace Contracts.Dtos.User.Post
{
    public class PostUserDto
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Username maximum length is 50 characters")]
        public string Username { get; set; }


        [Required(AllowEmptyStrings = false,ErrorMessage = "Password is required")]
        [PasswordValidation]
        public string Password { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name maximum length is 50 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        [MaxLength(50, ErrorMessage = "Last Name maximum length is 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Profile type is required")]
        [RoleNameValidator(RolesConstants.Consumer,RolesConstants.Deliverer,
            ErrorMessage = "RoleName must have one of the following values " + RolesConstants.Deliverer + ", " + RolesConstants.Consumer)]
        public string RoleName { get; set; }

        public IFormFile Image { get; set; }
    }
}
