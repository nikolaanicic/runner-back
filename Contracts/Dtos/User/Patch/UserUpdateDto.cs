using Contracts.Dtos.User.Post.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.User.Patch
{
    public class UserUpdateDto : PatchDtoValidator
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [PasswordValidation]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Not a valid email")]
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

    }
}
