using CarBook.Application.Features.CQRS.Commands.ContactCommands;
using CarBook.Application.Features.CQRS.Handlers.ContactHandlers;
using CarBook.Application.Features.CQRS.Queries.ContactQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly GetContactQueryHandler _getContactQueryHandler;
        private readonly GetContactInboxOutboxQueryHandler _getContactInboxOutboxQueryHandler;
        private readonly GetContactByIdQueryHandler _getContactByIdQueryHandler;
        private readonly CreateContactCommandHandler _createContactCommandHandler;
        private readonly UpdateContactCommandHandler _updateContactCommandHandler;
        private readonly RemoveContactCommandHandler _removeContactCommandHandler;

        public ContactsController(GetContactQueryHandler getContactQueryHandler,
            GetContactInboxOutboxQueryHandler getContactInboxOutboxQueryHandler,
            GetContactByIdQueryHandler getContactByIdQueryHandler,
            CreateContactCommandHandler createContactCommandHandler,
            UpdateContactCommandHandler updateContactCommandHandler,
            RemoveContactCommandHandler removeContactCommandHandler)
        {
            _getContactQueryHandler = getContactQueryHandler;
            _getContactInboxOutboxQueryHandler = getContactInboxOutboxQueryHandler;
            _getContactByIdQueryHandler = getContactByIdQueryHandler;
            _createContactCommandHandler = createContactCommandHandler;
            _updateContactCommandHandler = updateContactCommandHandler;
            _removeContactCommandHandler = removeContactCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _getContactQueryHandler.Handle();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var value = await _getContactByIdQueryHandler.Hanlde(new GetContactByIdQuery(id));

            return Ok(value);
        }

        [HttpGet("Inbox")]
        public async Task<IActionResult> Inbox()
        {
            // true -> Gelen Kutusu
            var value = await _getContactInboxOutboxQueryHandler.Handle(new GetContactByMessageTypeQuery(true));

            return Ok(value);
        }

        [HttpGet("Outbox")]
        public async Task<IActionResult> Outbox()
        {
            // false -> Giden Kutusu
            var value = await _getContactInboxOutboxQueryHandler.Handle(new GetContactByMessageTypeQuery(false));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactCommand createContactCommand)
        {
            await _createContactCommandHandler.Handle(createContactCommand);

            return Ok("İletişim Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactCommand updateContactCommand)
        {
            await _updateContactCommandHandler.Handle(updateContactCommand);

            return Ok("İletişim Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveContact(Guid id)
        {
            await _removeContactCommandHandler.Handle(new RemoveContactCommand(id));

            return Ok("İletişim Bilgisi Başarıyla Silindi.");
        }
    }
}
