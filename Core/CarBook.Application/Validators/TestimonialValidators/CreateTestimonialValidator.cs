using CarBook.Application.Features.Mediator.Commands.TestimonialCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.TestimonialValidators
{
    public class CreateTestimonialValidator : AbstractValidator<CreateTestimonialCommand>
    {
        public CreateTestimonialValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Alanı Boş Bırakılamaz!");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Unvan Alanı Boş Bırakılamaz!");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Yorum Alanı Boş Bırakılamaz!");
        }
    }
}
