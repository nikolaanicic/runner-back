using Contracts.Dtos.User.Post.Validators;
using System.ComponentModel.DataAnnotations;


namespace Contracts.Dtos.User.Post
{
    public class PostUserLogInDto
    {

        [Required(AllowEmptyStrings = false,ErrorMessage = "Email address can't be empty")]
        [MaxLength(50,ErrorMessage = "Email can be at most 50 characters long.")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
    }
}
