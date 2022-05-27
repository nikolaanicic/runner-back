using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos
{
    public interface ICanBeValidated
    {
        List<ValidationResult> IsValid(List<string> propsToValidate);

    }
}
