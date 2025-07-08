using CarBook.Dto.IdentityDtos;
using FluentValidation;

namespace CarBook.WebUI.Validators.IdentityValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mevcut Şifre Alanı Boş Bırakılamaz!");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Yeni Şifre Alanı Boş Bırakılamaz!");
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage("Yeni Şifre Tekrar Alanı Boş Bırakılamaz!");

            RuleFor(x => x.NewPassword).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
                .WithMessage("Şifre En Az 1 Karakter Küçük Harf, 1 Büyük Harf 1 Rakam ve 1 Özel Karakter İçermelidir ve En Az 8 Karakter Uzunluğunda Olmalıdır!");

            RuleFor(x => x.ConfirmNewPassword).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
                .WithMessage("Şifre En Az 1 Karakter Küçük Harf, 1 Büyük Harf 1 Rakam ve 1 Özel Karakter İçermelidir ve En Az 8 Karakter Uzunluğunda Olmalıdır!");

            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmNewPassword).WithMessage("Şifreleriniz Eşleşmemektedir!");
        }
    }
}
