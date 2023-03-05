using FluentValidation;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Common.Validations.ValidationRules
{
    public class MovieValidatons : AbstractValidator<MovieCreateDto>
    {
        public MovieValidatons()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Film adı boş geçilemez.").MaximumLength(100).WithMessage("Film adı 60 karakteri geçemez.");
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Film türü boş geçilemez.");
            RuleFor(x => x.DirectorId).NotEmpty().WithMessage("Yönetmen adı boş geçilemez.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Film fiyatı boş geçilemez.").GreaterThan(0).WithMessage("Film fiyatı sıfırdan büyük olmalı.");
            RuleFor(x => x.PublishYear).NotEmpty().WithMessage("Film yayınlanma boş geçilemez.");
            RuleFor(x => x.ActorIds).NotEmpty().WithMessage("Film oyuncuları boş geçilemez.");
        }
    }
}
