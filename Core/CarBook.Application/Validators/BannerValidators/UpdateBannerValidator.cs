using CarBook.Application.Features.CQRS.Commands.BannerCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.BannerValidators
{
    public class UpdateBannerValidator : AbstractValidator<UpdateBannerCommand>
    {
        public UpdateBannerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı Boş Olamaz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Olamaz!");
            RuleFor(x => x.Description).MinimumLength(20).WithMessage("Açıklama Alanı En Az 20 Karakter Olmalıdır!");
            RuleFor(x => x.VideoDescription).NotEmpty().WithMessage("Video Açıklama Alanı Boş Olamaz!");
            RuleFor(x => x.VideoURL).NotEmpty().WithMessage("Video URL Alanı Boş Olamaz!");
        }
    }
}
