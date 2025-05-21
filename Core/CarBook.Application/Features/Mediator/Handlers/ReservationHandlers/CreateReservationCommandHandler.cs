using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.RentACarCommands;
using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using CarBook.Application.Features.Mediator.Handlers.RentACarHandlers;
using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, HandlerResponseOptions>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IRepository<RentACar> _rentACarRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateReservationCommandHandler> _logger;

        public CreateReservationCommandHandler(IRepository<Reservation> repository, IMapper mapper, ILogger<CreateReservationCommandHandler> logger, IMediator mediator, IRepository<RentACar> rentACarRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _rentACarRepository = rentACarRepository;
        }

        public async Task<HandlerResponseOptions> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            request.ReservationID = Guid.NewGuid();

            Reservation valueToDto = _mapper.Map<Reservation>(request);

            var valueForRentACar = await _mediator.Send(new GetRentACarByCarIdQuery(request.CarID));

            if (valueForRentACar == null)
            {
                _logger.LogWarning($"Rezervasyon oluşturulamadı. {request.CarID} Araç Bulunamadı!");

                return new HandlerResponseOptions
                {
                    ResponseState = false,
                    ResponseMessage = "Rezervasyon oluşturulamadı. Rezervasyon Yapılacak Araç Bulunamadı!"
                };
            }

            CreateRentACarCommand createRentACarCommand = new CreateRentACarCommand();

            try
            {
                // Rezervasyon Verisi DB'ye Kaydedilir.
                await _repository.CreateAsync(valueToDto);

                // Rezervasyon Verilerine Göre RentACars tablosuna ilk insert işlemi yapılır.                

                createRentACarCommand.RentACarID = Guid.NewGuid();
                createRentACarCommand.Available = true;
                createRentACarCommand.CarID = request.CarID;
                createRentACarCommand.ReservationID = request.ReservationID;
                createRentACarCommand.PickUpDate = request.DropOffDate;
                createRentACarCommand.DropOffDate = new DateTimeOffset(Convert.ToDateTime("2099-01-01 00:00:00.000 +0300"), TimeSpan.FromHours(3));
                createRentACarCommand.LocationID = request.DropOffLocationID;

                RentACar valueToDtoCreateRentACarCommand = _mapper.Map<RentACar>(createRentACarCommand);

                await _rentACarRepository.CreateAsync(valueToDtoCreateRentACarCommand);

                /*
                 * Rezervasyon verisi kullanılarak,
                 * RentCars tablosunun PickUpDate tarihinden küçük ve büyük ilk veriler alınır.
                 * Kendinden küçük olan veri için Rezervasyon verisinin PickUpDate değeri RentACars tablosunun DropOffDate'ine yazılır.
                 * Kendinden büyük olanın Rezervasyon verisinden PickUpDate kendisinin DropOffDate'ine yazılır.
                */

                DbQueryOptions<RentACar> dbQueryOptionsForRentACar = new DbQueryOptions<RentACar>();

                // Filters
                Expression<Func<RentACar, bool>> filter1 = x => x.PickUpDate > request.DropOffDate && x.CarID == request.CarID; // kendinden büyük
                Expression<Func<RentACar, bool>> filter2 = x => x.PickUpDate < request.DropOffDate && x.CarID == request.CarID; // kendinden küçük
                Expression<Func<RentACar, bool>> filter4 = x => x.PickUpDate < request.DropOffDate && x.CarID == request.CarID
                && x.DropOffDate == new DateTimeOffset(Convert.ToDateTime("2099-01-01 00:00:00.000 +0300"), TimeSpan.FromHours(3)); // kendinden küçük v2
                Expression<Func<RentACar, bool>> filter3 = x => x.RentACarID == createRentACarCommand.RentACarID;

                // Foreign Keys
                List<Expression<Func<RentACar, object>>> includes = new List<Expression<Func<RentACar, object>>>
                {
                    x => x.Reservation
                };

                dbQueryOptionsForRentACar.filter = filter1;
                dbQueryOptionsForRentACar.includes = includes;
                var value1 = _rentACarRepository.GetQueryableEntity(dbQueryOptionsForRentACar).OrderByDescending(x => x.PickUpDate).Take(1).FirstOrDefault();
                dbQueryOptionsForRentACar.includes = new List<Expression<Func<RentACar, object>>> { };

                dbQueryOptionsForRentACar.filter = filter2;
                var value2 = _rentACarRepository.GetQueryableEntity(dbQueryOptionsForRentACar).OrderBy(x => x.PickUpDate).Take(1).FirstOrDefault();

                dbQueryOptionsForRentACar.filter = filter3;
                var value3 = _rentACarRepository.GetQueryableEntity(dbQueryOptionsForRentACar).FirstOrDefault();

                dbQueryOptionsForRentACar.filter = filter4;
                var value4 = _rentACarRepository.GetQueryableEntity(dbQueryOptionsForRentACar).OrderBy(x => x.PickUpDate).Take(1).FirstOrDefault();

                if (value4 != null)
                {
                    value4.DropOffDate = request.PickUpDate;
                    await _rentACarRepository.UpdateAsync(value4);
                }
                else
                {
                    if (value2 != null)
                    {
                        value2.DropOffDate = request.PickUpDate;
                        await _rentACarRepository.UpdateAsync(value2);
                    }
                }

                if (value3 != null && value1 != null)
                {
                    if (value1.Reservation != null)
                    {
                        value3.DropOffDate = value1.Reservation.PickUpDate;
                        await _rentACarRepository.UpdateAsync(value3);
                    }
                }

                _logger.LogInformation("Rezervasyon Başarıyla Oluşturuldu!");

                return new HandlerResponseOptions
                {
                    ResponseState = true,
                    ResponseMessage = "Rezervasyon Başarıyla Oluşturuldu!"
                };
            }
            catch (Exception ex)
            {
                // Herhangi Bir Hata Oluşması Durumunda Oluşturulan rezervasyon Verisi ve RentACars Verisi DB'den Silinir.
                DbQueryOptions<Reservation> dbQueryOptions = new DbQueryOptions<Reservation>();
                Expression<Func<Reservation, bool>> filter = x => x.ReservationID == request.ReservationID;
                dbQueryOptions.filter = filter;
                Reservation value = await _repository.GetByIdAsync(dbQueryOptions);

                DbQueryOptions<RentACar> dbQueryOptionsForRentACar = new DbQueryOptions<RentACar>();
                Expression<Func<RentACar, bool>> filterForRentACar = x => x.RentACarID == createRentACarCommand.RentACarID;
                dbQueryOptionsForRentACar.filter = filterForRentACar;
                RentACar getDataForRentACar = await _rentACarRepository.GetByIdAsync(dbQueryOptionsForRentACar);

                await _repository.RemoveAsync(value);
                await _rentACarRepository.RemoveAsync(getDataForRentACar);

                _logger.LogError($"Hata: {ex} - Rezervasyon oluşturulamadı. {request.CarID} Araç İçin Araç Durumu Değiştirilemedi İşlem Geri Alındı!");

                return new HandlerResponseOptions
                {
                    ResponseState = false,
                    ResponseMessage = "Rezervasyon oluşturulamadı. İşlem Geri Alındı!"
                };
            }
        }
    }
}
