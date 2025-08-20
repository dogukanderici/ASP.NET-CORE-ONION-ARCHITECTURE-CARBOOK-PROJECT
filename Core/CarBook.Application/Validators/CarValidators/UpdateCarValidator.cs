using CarBook.Application.Features.CQRS.Commands.CarCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Validators.CarValidators
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.BrandID).NotEmpty().WithMessage("Marka Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.Model).MinimumLength(5).WithMessage("Model Bilgisi En Az 5 Karakter Olmalıdır!");
            RuleFor(x => x.CoverImageURL).NotEmpty().WithMessage("Cover Image URL Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.KM).NotEmpty().WithMessage("Araç KM Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.KM).GreaterThan(0).WithMessage("Araç KM Bilgisi 0'dan ( sıfır ) Küçük Olamaz!");
            RuleFor(x => x.Transmission).NotEmpty().WithMessage("Şanzıman Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.Seat).NotEmpty().WithMessage("Koltuk Sayısı Bilgisi Boş Bırakılamaz!");
            RuleFor(x => x.Seat).GreaterThanOrEqualTo(2).WithMessage("Koltuk Sayısı Bilgisi 2'den Küçük Olamaz!");
            RuleFor(x => x.Luggage).NotEmpty().WithMessage("Bagaj Bilgisi Boş Olamaz!");
            RuleFor(x => x.Fuel).NotEmpty().WithMessage("Yakıt Türü Bilgisi Boş Olamaz!");
            RuleFor(x => x.BigImageURL).NotEmpty().WithMessage("Big Image URL Bilgisi Boş Olamaz!");
        }
    }
}
