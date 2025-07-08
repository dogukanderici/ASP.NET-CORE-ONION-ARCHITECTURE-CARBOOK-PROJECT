using CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.BlogCategoryValidators
{
    public class UpdateBlogCategoryValidator : AbstractValidator<UpdateBlogCategoryCommand>
    {
        public UpdateBlogCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Blog Kategori Adı Alanı Boş Olamaz!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Blog Kategori Adı En Az 3 Karakterden Oluşmalıdır!");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Blog Kategori Adı En Fazla 20 Karekterden Oluşabilir!");

            RuleFor(x => x.Name).Must(val => IsNumeric(val)).WithMessage("Blog Adı Sadece Metin Değer Alabilir!");
        }

        private bool IsNumeric(string val)
        {
            return val is string;
        }
    }
}
