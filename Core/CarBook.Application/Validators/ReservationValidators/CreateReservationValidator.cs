using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarBook.Application.Validators.ReservationValidators
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Alanı Boş Olamaz!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad Alanı Boş Olamaz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta Alanı Boş Olamaz!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon Alanı Boş Olamaz!");
            RuleFor(x => x.CarID).NotEmpty().WithMessage("Araç Bilgisi Alanı Boş Olamaz!");
            RuleFor(x => x.PickUpLocationID).NotEmpty().WithMessage("Alım Lokasyon Bilgisi Alanı Boş Olamaz!");
            RuleFor(x => x.DropOffLocationID).NotEmpty().WithMessage("Teslim Yeri Lokasyon Bilgisi Alanı Boş Olamaz!");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Sürücü Yaşı Alanı Boş Olamaz!");
            RuleFor(x => x.DriverLicenceAge).NotEmpty().WithMessage("Ehliyet Yaşı Alanı Boş Olamaz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Olamaz!");
            RuleFor(x => x.PickUpDate).NotEmpty().WithMessage("Alım Tarihi Alanı Boş Olamaz!");
            RuleFor(x => x.DropOffDate).NotEmpty().WithMessage("Teslim Tarihi Alanı Boş Olamaz!");

            RuleFor(x => x.PickUpDate).GreaterThanOrEqualTo(DateTimeOffset.Now).WithMessage("Alım Tarihi Alanı Bugünden Büyük Olamaz!");
            RuleFor(x => x.PickUpDate).GreaterThanOrEqualTo(x => x.DropOffDate).WithMessage("Alım Tarihi Alanı Teslim Tarihinden Büyük Olamaz!");
            RuleFor(x => x.DropOffDate).LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("Teslim Tarihi Alanı Bugünden Küçük Olamaz!");

            RuleFor(x => x.Age).LessThan(18).WithMessage("Sürücü Yaşı Alanı 18'den Küçük Olamaz!");
            RuleFor(x => x.DriverLicenceAge).LessThan(1).WithMessage("Ehliyet Yaşı Alanı 1'den Küçük Olamaz!");
            RuleFor(x => x.DriverLicenceAge).LessThan(x => x.Age).WithMessage("Ehliyet Yaşı Alanı Sürücü Yaşından Büyük Olamaz!");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli Bir E-Posta Adresi Girilmelidir!");
        }
    }
}
