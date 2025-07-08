using CarBook.Application.Features.CQRS.Commands.CarPricingCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.CarPricingValidators
{
    public class CreateCarPricingValidator : AbstractValidator<CreateCarPricingCommand>
    {
        public CreateCarPricingValidator()
        {
            RuleFor(x => x.CarID).NotEmpty().WithMessage("Araç Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.PricingTypeID).NotEmpty().WithMessage("Ücret Tipi Boş Bırakılamaz!");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Ücret Boş Bırakılamaz!");
            RuleFor(x => x.Amount).LessThan(0).WithMessage("Ücret 0'dan ( sıfır ) Küçük Olamaz!");
        }
    }
}
