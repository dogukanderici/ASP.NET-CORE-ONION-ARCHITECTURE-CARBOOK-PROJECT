﻿using CarBook.Application.Features.CQRS.Commands.BannerCommands;
using CarBook.Application.Features.CQRS.Handlers.BannerHandlers;
using CarBook.Application.Features.CQRS.Queries.BannerQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly GetBannerQueryHandler _getBannerQueryHandler;
        private readonly GetBannerByIdQueryHandler _getBannerByIdQueryHandler;
        private readonly CreateBannerCommandHandler _createBannerCommandHandler;
        private readonly UpdateBannerCommandHandler _updateBannerCommandHandler;
        private readonly RemoveBannerCommandHandler _removeBannerCommandHandler;

        public BannersController(GetBannerQueryHandler getBannerQueryHandler,
            GetBannerByIdQueryHandler getBannerByIdQueryHandler,
            CreateBannerCommandHandler createBannerCommandHandler,
            UpdateBannerCommandHandler updateBannerCommandHandler,
            RemoveBannerCommandHandler removeBannerCommandHandler)
        {
            _getBannerQueryHandler = getBannerQueryHandler;
            _getBannerByIdQueryHandler = getBannerByIdQueryHandler;
            _createBannerCommandHandler = createBannerCommandHandler;
            _updateBannerCommandHandler = updateBannerCommandHandler;
            _removeBannerCommandHandler = removeBannerCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> BannertList()
        {
            var values = await _getBannerQueryHandler.Handle();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBanner(int id)
        {
            var value = await _getBannerByIdQueryHandler.Handle(new GetBannerByIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerCommand createBannerCommand)
        {
            await _createBannerCommandHandler.Handle(createBannerCommand);

            return Ok("Banner Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBanner(UpdateBannerCommand updateBannerCommand)
        {
            await _updateBannerCommandHandler.Handle(updateBannerCommand);

            return Ok("Banner Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            await _removeBannerCommandHandler.Handle(new RemoveBannerCommand(id));

            return Ok("Banner Bilgisi Başarıyla Silindi.");
        }
    }
}
