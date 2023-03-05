using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStoreWebApi.Entities.Concrete;

namespace MovieStoreWebApi.Common.Validations.ValidationRules.Director
{
    public class DirectorValidations : AbstractValidator<Entities.Concrete.Director>
    {
        public DirectorValidations()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Yönetmen adı boş olamaz");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Yönetmen adı boş olamaz");
        }
    }
}
