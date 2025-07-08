using CarBook.Application.Features.Mediator.Commands.LocationCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.LocationValidators
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationValidator()
        {
            RuleFor(x => x.LocationName).NotEmpty().WithMessage("Lokasyon Adı Boş Olamaz!");
            RuleFor(x => x.LocationName).MinimumLength(3).WithMessage("Lokasyon Adı En Az 3 Karakter Olmalıdır!");
            RuleFor(x => x.LocationName).MaximumLength(15).WithMessage("Lokasyon Adı En Fazla 15 Karakter Olmalıdır!");
        }
    }
}
