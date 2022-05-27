using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Contracts.Dtos
{

    /// <summary>
    /// This class provides validation for the patch dtos
    /// Because patch requests are handled with json patch documents
    /// This class exists because the documents and the values they change are not validated by default
    /// </summary>
    public abstract class PatchDtoValidator : ICanBeValidated
    {

        /// <summary>
        /// Creates a validation context and validates wanted properties 
        /// </summary>
        /// <param name="propsToValidate"></param>
        /// <returns></returns>
        public List<ValidationResult> IsValid(List<string> propsToValidate)
        {
            var validationContext = new ValidationContext(this);

            var results = new List<ValidationResult>();

            foreach(var prop in propsToValidate)
            {
                var attrs = GetValidationAttribute(prop);
                Validator.TryValidateValue(GetPropertyValue(prop), validationContext, results, attrs);
            }

            return results;
        }


        /// <summary>
        /// Gets validation attributes from a wanted property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private List<ValidationAttribute> GetValidationAttribute(string property)
        {
            var objAttrs = GetType().GetProperty(property)?.GetCustomAttributes(false);
            return (from x in objAttrs select (ValidationAttribute)x).ToList();
        }


        /// <summary>
        /// Gets the property value
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private object GetPropertyValue(string property)
        {
            return GetType().GetProperty(property).GetValue(this);
        }
    }
}
