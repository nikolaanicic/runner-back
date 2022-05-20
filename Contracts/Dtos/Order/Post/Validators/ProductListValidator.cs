using Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.Order.Post.Validators
{

    /// <summary>
    /// Provides validation for the list of product ids sent from the frontend application
    /// </summary>
    public class ProductListValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            string message = "Product list can't be empty";

            try
            {
                List<long> ids = (List<long>)Convert.ChangeType(value, typeof(List<long>));

                if (value == null || ids.Count == 0)
                    throw new BadRequestException(message);
            }
            catch(Exception e)
            {
                throw new BadRequestException(e.Message);
            }

            return ValidationResult.Success;
        }
    }
}
