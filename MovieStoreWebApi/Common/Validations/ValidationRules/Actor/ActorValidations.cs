using FluentValidation;
using MovieStoreWebApi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Common.Validations.ValidationRules
{
    public class ActorValidations : AbstractValidator<Actor>
    {
        public ActorValidations()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Oyuncu adı boş geçilemez.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Oyuncu soyadı boş geçilemez.");
        }
    }
}
