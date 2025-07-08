using CarBook.Application.Features.Mediator.Commands.SocialMediaCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.SocialMediaValidators
{
    public class UpdateSocialMediaValidator : AbstractValidator<UpdateSocialMediaCommand>
    {
        public UpdateSocialMediaValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Alanı Boş Olamaz!");
            RuleFor(x => x.Url).NotEmpty().WithMessage("URL Alanı Boş Olamaz!");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("İkon Alanı Boş Olamaz!");
        }
    }
}
