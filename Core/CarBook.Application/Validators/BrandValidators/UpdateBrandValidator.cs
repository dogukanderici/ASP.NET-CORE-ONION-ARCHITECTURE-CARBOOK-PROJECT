using CarBook.Application.Features.CQRS.Commands.BrandCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.BrandValidators
{
    public class UpdateBrandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandValidator()
        {
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("Marka Adı Bilgisi Boş Olamaz!");
            RuleFor(x => x.BrandName).MinimumLength(3).WithMessage("Marka Adı En Az 3 Karakter Olmalıdır!");
            RuleFor(x => x.BrandName).MaximumLength(20).WithMessage("Marka Adı En Fazla 20 Karakter Olmalıdır!");
        }
    }
}
