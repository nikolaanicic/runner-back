using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Contracts.Dtos.User.Post.Validators
{

    /// <summary>
    /// This class is the custom validator for roles in dtos
    /// When role is sent from frontend to the backend it should have one of two values
    /// New user can either be a consumer or a deliverer in the app
    /// Admins are added by the maintainters of the application
    /// </summary>
    public class RoleNameValidator: ValidationAttribute
    {

        string[] allowed;

        public RoleNameValidator(params string[] allowedRoles)
        {
            allowed = allowedRoles;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string message = $"Role must be on of the following values{string.Join(',', allowed)}";

            if (value == null || !allowed.Contains(value))
                return new ValidationResult(message);

            return ValidationResult.Success;
        }
    }
}
