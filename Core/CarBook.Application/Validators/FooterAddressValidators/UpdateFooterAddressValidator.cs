using CarBook.Application.Features.Mediator.Commands.FooterAddressCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.FooterAddressValidators
{
    public class UpdateFooterAddressValidator : AbstractValidator<UpdateFooterAddressCommand>
    {
        public UpdateFooterAddressValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Boş Olamaz!");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Açıklama En Az 10 Karakter Olmalıdır!");
            RuleFor(x => x.Description).MaximumLength(50).WithMessage("Açıklama En Fazla 50 Karakter Olmalıdır!");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres Boş Olamaz!");
            RuleFor(x => x.Address).MinimumLength(10).WithMessage("Adres En Az 10 Karakter Olmalıdır!");
            RuleFor(x => x.Address).MaximumLength(50).WithMessage("Adres En Fazla 50 Karakter Olmalıdır!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon Boş Olamaz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta Boş Olamaz!");
        }
    }
}
