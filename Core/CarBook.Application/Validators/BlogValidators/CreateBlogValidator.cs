using CarBook.Application.Features.Mediator.Commands.BlogCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.BlogValidators
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Blog Başlık Alanı Boş Olamaz!");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Blog Başlığı En Az 5 Karakterden Oluşmalıdır!");
            RuleFor(x => x.Title).MaximumLength(25).WithMessage("Blog Başlığı En Fazla 25 Karakterden Oluşmalıdır!");

            RuleFor(x => x.AuthorID).NotEmpty().WithMessage("Blog Yazar Bilgisi Boş Olamaz!");
            RuleFor(x => x.CoverImageUrl).NotEmpty().WithMessage("Blog Görsel Bilgisi Boş Olamaz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Blog Açıklaması Bilgisi Boş Olamaz!");
            RuleFor(x => x.CreatedDate).NotEmpty().WithMessage("Blog Oluşturulma Tarihi Bilgisi Boş Olamaz!");
            RuleFor(x => x.BlogCategoryID).NotEmpty().WithMessage("Blog Kategori Bilgisi Boş Olamaz!");

            RuleFor(x => x.CreatedDate).GreaterThan(DateTimeOffset.Now).WithMessage("Blog Oluşturulma Tarihi Bugünden Büyük Olamaz!");
        }
    }
}
