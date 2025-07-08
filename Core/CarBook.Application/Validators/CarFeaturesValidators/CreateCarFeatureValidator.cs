using CarBook.Application.Features.CQRS.Commands.CarFeatureCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.CarFeaturesValidators
{
    public class CreateCarFeatureValidator : AbstractValidator<CreateCarFeatureCommand>
    {
        public CreateCarFeatureValidator()
        {
            RuleFor(x => x.CarID).NotEmpty().WithMessage("Araç Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.FeatureID).NotEmpty().WithMessage("Araç Özellik Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.Available).NotEmpty().WithMessage("Araç Kullanılabilirlik Bilgisi Boş Bırakılamaz!");
        }
    }
}
