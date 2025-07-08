using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Models;
using FluentValidation;

namespace CarBook.WebUI.Validators.IdentityValidators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş Bırakılamaz!");
        }
    }
}
