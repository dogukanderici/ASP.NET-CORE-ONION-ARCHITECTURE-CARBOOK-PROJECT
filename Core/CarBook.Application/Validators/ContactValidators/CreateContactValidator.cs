using CarBook.Application.Features.CQRS.Commands.ContactCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.ContactValidators
{
    public class CreateContactValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad / Soyad Alanı Boş Olamaz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta Alanı Boş Olamaz!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu Alanı Boş Olamaz!");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj Alanı Boş Olamaz!");
            //RuleFor(x => x.MessageType).NotEmpty().WithMessage("Mesaj Tipi Alanı Boş Olamaz!");
        }
    }
}
