using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Common.Validations.ValidationTool
{
    public class ValidationTool
    {

        public static void Validate(IValidator validator, object context)
        {
            var validationContext = new ValidationContext<object>(context);
            var result = validator.Validate(validationContext);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
