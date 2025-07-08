using CarBook.Application.Features.Mediator.Queries.StatisticsQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAuthorCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetAuthorCount()
        {
            var value = await _mediator.Send(new GetAuthorCountQuery());

            return Ok(value);
        }

        [HttpGet("GetAvgRentPriceForDailyCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetAvgRentPriceForDailyCount()
        {
            var value = await _mediator.Send(new GetAvgRentPriceForDailyCountQuery());

            return Ok(value);
        }

        [HttpGet("GetAvgRentPriceForHourlyCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetAvgRentPriceForHourlyCount()
        {
            var value = await _mediator.Send(new GetAvgRentPriceForHourlyCountQuery());

            return Ok(value);
        }

        [HttpGet("GetAvgRentPriceForMountlyCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetAvgRentPriceForMountlyCount()
        {
            var value = await _mediator.Send(new GetAvgRentPriceForMountlyCountQuery());

            return Ok(value);
        }

        [HttpGet("GetAvgRentPriceForWeeklyCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetAvgRentPriceForWeeklyCount()
        {
            var value = await _mediator.Send(new GetAvgRentPriceForWeeklyCountQuery());

            return Ok(value);
        }

        [HttpGet("GetBlogCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetBlogCount()
        {
            var value = await _mediator.Send(new GetBlogCountQuery());

            return Ok(value);
        }

        [HttpGet("GetBlogTitleByMaxBlogComment")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetBlogTitleByMaxBlogComment()
        {
            var value = await _mediator.Send(new GetBlogTitleByMaxBlogCommentQuery());

            return Ok(value);
        }

        [HttpGet("GetBrandCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetBrandCount()
        {
            var value = await _mediator.Send(new GetBrandCountQuery());

            return Ok(value);
        }

        [HttpGet("GetBrandNameByMaxCar")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetBrandNameByMaxCar()
        {
            var value = await _mediator.Send(new GetBrandNameByMaxCarQuery());

            return Ok(value);
        }

        [HttpGet("GetCarBrandAndModelByPriceDailyMax")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarBrandAndModelByPriceDailyMax()
        {
            var value = await _mediator.Send(new GetCarBrandAndModelByPriceDailyMaxQuery());

            return Ok(value);
        }

        [HttpGet("GetCarBrandAndModelByPriceDailyMin")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarBrandAndModelByPriceDailyMin()
        {
            var value = await _mediator.Send(new GetCarBrandAndModelByPriceDailyMinQuery());

            return Ok(value);
        }

        [HttpGet("GetCarCountByFuelElectric")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarCountByFuelElectric()
        {
            var value = await _mediator.Send(new GetCarCountByFuelElectricQuery());

            return Ok(value);
        }

        [HttpGet("GetCarCountByFuelGasOrDiesel")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarCountByFuelGasOrDiesel()
        {
            var value = await _mediator.Send(new GetCarCountByFuelGasOrDieselQuery());

            return Ok(value);
        }

        [HttpGet("GetCarCountByKmSmallerThan1000")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarCountByKmSmallerThan1000()
        {
            var value = await _mediator.Send(new GetCarCountByKmSmallerThan1000Query());

            return Ok(value);
        }

        [HttpGet("GetCarCountByTransmissionIsAuto")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarCountByTransmissionIsAuto()
        {
            var value = await _mediator.Send(new GetCarCountByTransmissionIsAutoQuery());

            return Ok(value);
        }

        [HttpGet("GetCarCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetCarCount()
        {
            var value = await _mediator.Send(new GetCarCountQuery());

            return Ok(value);
        }

        [HttpGet("GetLocationCount")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetLocationCount()
        {
            var value = await _mediator.Send(new GetLocationCountQuery());

            return Ok(value);
        }
    }
}
