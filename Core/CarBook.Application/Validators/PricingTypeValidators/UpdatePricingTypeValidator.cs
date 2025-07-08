using CarBook.Application.Features.Mediator.Commands.PricingTypeCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.PricingTypeValidators
{
    public class UpdatePricingTypeValidator : AbstractValidator<UpdatePricingTypeCommand>
    {
        public UpdatePricingTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ödeme Tipi Adı Boş Olamaz!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ödeme Tipi Adı En Az 3 Karakter Olmalıdır!");
            RuleFor(x => x.Name).MaximumLength(10).WithMessage("Ödeme Tipi Adı En Fazla 10 Karakter Olmalıdır!");
        }
    }
}
