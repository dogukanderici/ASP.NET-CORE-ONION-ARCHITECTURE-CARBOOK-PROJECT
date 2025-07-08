using CarBook.Application.Features.Mediator.Commands.ServiceCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.ServiceValidators
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı Boş Olamaz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Olamaz!");
            RuleFor(x => x.IconURL).NotEmpty().WithMessage("İkon URL Alanı Boş Olamaz!");

            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Başlık Alanı En Az 3 Karakter Olmalıdır!");
            RuleFor(x => x.Title).MaximumLength(10).WithMessage("Başlık Alanı En Fazla 10 Karakter Olmalıdır!");

            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Açıklama Alanı En Az 10 Karakter Olmalıdır!");
            RuleFor(x => x.Description).MaximumLength(20).WithMessage("Açıklama Alanı En Fazla 20 Karakter Olmalıdır!");
        }
    }
}
