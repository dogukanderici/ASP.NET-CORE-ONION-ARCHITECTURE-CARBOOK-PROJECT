using CarBook.Dto.AboutDtos;
using FluentValidation;

namespace CarBook.WebUI.Validators.AboutValidators
{
    public class CreateAboutValidator : AbstractValidator<CreateAboutDto>
    {
        public CreateAboutValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı Boş Olamaz!");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Başlık Alanı En Az 5 Karakterden Oluşmalıdır!");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Başlık Alanı En Fazla 50 Karakterden Oluşmalıdır!");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Bırakılamaz!");
            RuleFor(x => x.Description).MinimumLength(20).WithMessage("Açıklama Alanı En Az 20 Karakterden Oluşmalıdır!");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama Alanı En Fazla 100 Karakterden Oluşmalıdır!");

            RuleFor(x => x.Image).NotEmpty().WithMessage("Görsel URL Alanı Boş Bırakılamaz!");
        }
    }
}
