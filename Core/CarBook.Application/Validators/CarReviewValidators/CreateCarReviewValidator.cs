using CarBook.Application.Features.CQRS.Commands.CarReviewCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.CarReviewValidators
{
    public class CreateCarReviewValidator : AbstractValidator<CreateCarReviewCommand>
    {
        public CreateCarReviewValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Adınızı Giriniz!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Lütfen Adınızı En Az 3 Karakter Giriniz!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Lütfen Soyadınızı Giriniz!");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Lütfen Soyadınızı En Az 3 Karakter Giriniz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen E-Posta Adresinizi Giriniz!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen Geçerli Bie E-Posta Adresi Giriniz!");
            RuleFor(x => x.CommentStar).GreaterThan(0).WithMessage("Değerlendirmenizi En Az 1 Puan Verebilirsiniz!");
            RuleFor(x => x.CommentStar).LessThan(6).WithMessage("Değerlendirmenizi En Fazla 5 Puan Verebilirsiniz!");
            RuleFor(x => x.Comment).MaximumLength(1000).WithMessage("1000 Karakterden Fazla Yorum Giremezsiniz!");
        }
    }
}
