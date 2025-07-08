using CarBook.Application.Features.Mediator.Commands.TagCloudCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.TagCloudValidator
{
    public class CreateTagCloudValidator : AbstractValidator<CreateTagCloudCommand>
    {
        public CreateTagCloudValidator()
        {
            RuleFor(x => x.TagName).NotEmpty().WithMessage("Etiket Adı Alanı Boş Bırakılamaz!");
            RuleFor(x => x.TagStatus).NotEmpty().WithMessage("Etiket Durumu Alanı Boş Bırakılamaz!");
        }
    }
}
