using CarBook.Application.Features.Mediator.Commands.FeatureCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.FeatureValidators
{
    public class UpdateFeatureValidator : AbstractValidator<UpdateFeatureCommand>
    {
        public UpdateFeatureValidator()
        {
            RuleFor(x => x.FeatureName).NotEmpty().WithMessage("Özellik Adı Boş Olamaz!");
            RuleFor(x => x.FeatureName).MinimumLength(3).WithMessage("Özellik Adı En Az 3 Karakter Olmalıdır!");
            RuleFor(x => x.FeatureName).MaximumLength(25).WithMessage("Özellik Adı En Fazla 25 Karakter Olmalıdır!");
        }
    }
}
