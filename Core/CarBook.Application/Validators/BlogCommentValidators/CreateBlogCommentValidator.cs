using CarBook.Application.Features.Mediator.Commands.BlogCommentCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.BlogCommentValidators
{
    public class CreateBlogCommentValidator : AbstractValidator<CreateBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad/ Soyad Alanı Boş Olamaz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Yorum Alanı Boş Olamaz!");
            RuleFor(x => x.Description).MinimumLength(5).WithMessage("Yorum Alanı En Az 5 Karakter Olmalıdır!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Yorum Alanı En Fazla 500 Karakter Olmalıdır!");
            RuleFor(x => x.CreatedDate).NotEmpty().WithMessage("Yorum Oluşturulma Tarihi Boş Olamaz!");
            RuleFor(x => x.CreatedDate).GreaterThan(DateTimeOffset.Now).WithMessage("Yorum Oluşturulma Tarihi Bugünün Tarihinden Büyük Olamaz!");
            RuleFor(x => x.BlogID).NotEmpty().WithMessage("Yorum Yapılacak Blog Bilgisi Boş Olamaz!");
        }
    }
}
