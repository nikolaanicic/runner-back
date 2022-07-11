using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contracts.Dtos.User.Post.Validators
{

    /// <summary>
    /// Validates users password based on a simple regular expression
    /// 
    /// Password should be between 5 and 20 characters long
    /// Password can contains letters, numbers and the following characters (,),?,!,-,_
    /// And should have one of the following characters (,),?,!,-
    /// </summary>
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string message = "Invalid password. Password must be at least 5 characters long, at most 20. And must contain at least one digit.";

            string passwordPattern = @"^(?=.*\d)[a-zA-Z\d]{5,13}$";
            string password = (string)Convert.ChangeType(value, typeof(string));

            if (password == null || password.Length < 5 || password.Length > 20)
                return new ValidationResult(message);

            try
            {
                var matches = Regex.Match(password, passwordPattern);
                if (matches.Captures.Count!= 1)
                    return new ValidationResult(message);

            }catch(Exception e)
            {
                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}
