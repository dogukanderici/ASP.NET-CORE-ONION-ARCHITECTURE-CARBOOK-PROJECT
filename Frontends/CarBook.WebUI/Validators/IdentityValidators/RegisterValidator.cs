using CarBook.Dto.IdentityDtos;
using FluentValidation;

namespace CarBook.WebUI.Validators.IdentityValidators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Boş Bırakılamaz!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad Boş Olamaz!");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta Boş Olamaz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş Olamaz!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre Tekrarı Boş Olamaz!");

            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ad Alanı En Az 3 Karakter Olmalıdır!");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Ad Alanı En Fazla 15 Karakter Olmalıdır!");

            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Soyad Alanı En Az 2 Karakter Olmalıdır!");
            RuleFor(x => x.Surname).MaximumLength(15).WithMessage("Soyad Alanı En Fazla 15 Karakter Olmalıdır!");

            RuleFor(x => x.Email).EmailAddress().WithMessage("E-Posta Adresiniz Geçerli Bir Mail Adresi Olmalıdır!");

            RuleFor(x => x.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
                .WithMessage("Şifre En Az 1 Karakter Küçük Harf, 1 Büyük Harf 1 Rakam ve 1 Özel Karakter İçermelidir ve En Az 8 Karakter Uzunluğunda Olmalıdır!");

            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifreleriniz Eşleşmemektedir!");
        }
    }
}
