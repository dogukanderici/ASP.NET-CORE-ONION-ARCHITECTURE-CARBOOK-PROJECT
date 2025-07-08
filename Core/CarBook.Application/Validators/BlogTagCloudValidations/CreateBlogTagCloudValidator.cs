using CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.BlogTagCloudValidations
{
    public class CreateBlogTagCloudValidator : AbstractValidator<CreateBlogTagCloudCommand>
    {
        public CreateBlogTagCloudValidator()
        {
            RuleFor(x => x.BlogID).NotEmpty().WithMessage("Blog Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.TagCloudID).NotEmpty().WithMessage("Tag Bilgisi Boş Bırakılamaz!");
        }
    }
}
