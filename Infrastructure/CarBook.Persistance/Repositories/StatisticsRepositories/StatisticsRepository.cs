using CarBook.Application.Interfaces;
using CarBook.Application.Interfaces.StatisticsInterfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistance.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<BlogComment> _blogCommentRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<CarPricing> _carPricingRepository;

        public StatisticsRepository(
            IRepository<Author> authorRepository,
            IRepository<Blog> blogRepository,
            IRepository<BlogComment> blogCommentRepository,
            IRepository<Brand> brandRepository,
            IRepository<Car> carRepository,
            IRepository<Location> locationRepository,
            IRepository<CarPricing> carPricingRepository
            )
        {
            _authorRepository = authorRepository;
            _blogRepository = blogRepository;
            _blogCommentRepository = blogCommentRepository;
            _brandRepository = brandRepository;
            _carRepository = carRepository;
            _locationRepository = locationRepository;
            _carPricingRepository = carPricingRepository;
        }

        public int GetAuthorCount()
        {
            DbQueryOptions<Author> dbQueryOptions = new DbQueryOptions<Author>();

            var value = _authorRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public decimal GetAvgRentPriceForDailyCount()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.PricingType.Name == "Günlük";

            dbQueryOptions.filter = filter;

            var value = _carPricingRepository.GetQueryableEntity(dbQueryOptions).Average(x => x.Amount);

            return value;
        }

        public decimal GetAvgRentPriceForHourlyCount()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.PricingType.Name == "Saatlik";

            dbQueryOptions.filter = filter;

            var value = _carPricingRepository.GetQueryableEntity(dbQueryOptions).Average(x => x.Amount);

            return value;
        }

        public decimal GetAvgRentPriceForMountlyCount()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.PricingType.Name == "Aylık";

            dbQueryOptions.filter = filter;

            var value = _carPricingRepository.GetQueryableEntity(dbQueryOptions).Average(x => x.Amount);

            return value;
        }

        public decimal GetAvgRentPriceForWeeklyCount()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.PricingType.Name == "Haftalık";

            dbQueryOptions.filter = filter;

            var value = _carPricingRepository.GetQueryableEntity(dbQueryOptions).Average(x => x.Amount);

            return value;
        }

        public int GetBlogCount()
        {
            DbQueryOptions<Blog> dbQueryOptions = new DbQueryOptions<Blog>();

            var value = _blogRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public string GetBlogTitleByMaxBlogComment()
        {
            DbQueryOptions<BlogComment> dbQueryOptions = new DbQueryOptions<BlogComment>();

            var value = _blogCommentRepository.GetQueryableEntity(dbQueryOptions).GroupBy(x => new
            {
                x.BlogID,
                x.Blog.Title
            })
                .Select(y => new
                {
                    BlogID = y.Key.BlogID,
                    BlogTitle = y.Key.Title,
                    CommentCount = y.Count(),
                })
                .OrderByDescending(y => y.CommentCount).FirstOrDefault();

            return value.BlogTitle;
        }

        public int GetBrandCount()
        {
            DbQueryOptions<Brand> dbQueryOptions = new DbQueryOptions<Brand>();

            var value = _brandRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public string GetBrandNameByMaxCar()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            var value = _carRepository.GetQueryableEntity(dbQueryOptions).GroupBy(x => new
            {
                x.BrandID,
                x.Brand.BrandName
            })
                .Select(y => new
                {
                    BrandId = y.Key.BrandID,
                    BrandName = y.Key.BrandName,
                    BrandCount = y.Count()
                })
                .OrderByDescending(x => x.BrandCount).FirstOrDefault();

            return value.BrandName;
        }

        public string GetCarBrandAndModelByPriceDailyMax()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.PricingType.Name == "Günlük";

            dbQueryOptions.filter = filter;

            var value = _carPricingRepository.GetQueryableEntity(dbQueryOptions)
                .OrderByDescending(x => x.Amount)
                .Select(x => new
                {
                    BrandName = x.Car.Brand.BrandName,
                    Model = x.Car.Model
                }).FirstOrDefault();

            return $"{value.BrandName} - {value.Model}";
        }

        public string GetCarBrandAndModelByPriceDailyMin()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.PricingType.Name == "Günlük";

            dbQueryOptions.filter = filter;

            var value = _carPricingRepository.GetQueryableEntity(dbQueryOptions)
                .OrderBy(x => x.Amount)
                .Select(x => new
                {
                    BrandName = x.Car.Brand.BrandName,
                    Model = x.Car.Model
                })
                .FirstOrDefault();

            return $"{value.BrandName} - {value.Model}";
        }

        public int GetCarCount()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            var value = _carRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public int GetCarCountByFuelElectric()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            Expression<Func<Car, bool>> filter = x => x.Fuel == "Elektrik";

            dbQueryOptions.filter = filter;

            var value = _carRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public int GetCarCountByFuelGasOrDiesel()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            Expression<Func<Car, bool>> filter = x => x.Fuel == "Benzin" || x.Fuel == "Dizel";

            dbQueryOptions.filter = filter;

            var value = _carRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public int GetCarCountByKmSmallerThan1000()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            Expression<Func<Car, bool>> filter = x => x.KM < 1000;

            dbQueryOptions.filter = filter;

            var value = _carRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public int GetCarCountByTransmissionIsAuto()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            Expression<Func<Car, bool>> filter = x => x.Transmission == "Otomatik";

            dbQueryOptions.filter = filter;

            var value = _carRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }

        public int GetLocationCount()
        {
            DbQueryOptions<Location> dbQueryOptions = new DbQueryOptions<Location>();

            var value = _locationRepository.GetQueryableEntity(dbQueryOptions).Count();

            return value;
        }
    }
}
