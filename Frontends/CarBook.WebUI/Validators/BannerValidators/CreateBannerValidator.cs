using CarBook.Dto.BannerDtos;
using FluentValidation;

namespace CarBook.WebUI.Validators.BannerValidators
{
    public class CreateBannerValidator : AbstractValidator<CreateBannerDto>
    {
        public CreateBannerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı Boş Olamaz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Olamaz!");
            RuleFor(x => x.Description).MinimumLength(20).WithMessage("Açıklama Alanı En Az 20 Karakter Olmalıdır!");
            RuleFor(x => x.VideoDescription).NotEmpty().WithMessage("Video Açıklama Alanı Boş Olamaz!");
            RuleFor(x => x.VideoURL).NotEmpty().WithMessage("Video URL Alanı Boş Olamaz!");
        }
    }
}
