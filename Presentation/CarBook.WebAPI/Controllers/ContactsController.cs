using CarBook.Application.Features.CQRS.Commands.ContactCommands;
using CarBook.Application.Features.CQRS.Handlers.ContactHandlers;
using CarBook.Application.Features.CQRS.Queries.ContactQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IValidator<CreateContactCommand> _createValidator;
        private readonly IValidator<UpdateContactCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public ContactsController(GetContactQueryHandler getContactQueryHandler,
            GetContactInboxOutboxQueryHandler getContactInboxOutboxQueryHandler,
            GetContactByIdQueryHandler getContactByIdQueryHandler,
            CreateContactCommandHandler createContactCommandHandler,
            UpdateContactCommandHandler updateContactCommandHandler,
            RemoveContactCommandHandler removeContactCommandHandler,
            IValidator<CreateContactCommand> createValidator,
            IValidator<UpdateContactCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getContactQueryHandler = getContactQueryHandler;
            _getContactInboxOutboxQueryHandler = getContactInboxOutboxQueryHandler;
            _getContactByIdQueryHandler = getContactByIdQueryHandler;
            _createContactCommandHandler = createContactCommandHandler;
            _updateContactCommandHandler = updateContactCommandHandler;
            _removeContactCommandHandler = removeContactCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> ContactList()
        {
            var values = await _getContactQueryHandler.Handle();

            return Ok(values);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            try
            {
                var value = await _getContactByIdQueryHandler.Hanlde(new GetContactByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all contact message datas!");
            }
        }

        [HttpGet("Inbox")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> Inbox()
        {
            try
            {
                // true -> Gelen Kutusu
                var value = await _getContactInboxOutboxQueryHandler.Handle(new GetContactByMessageTypeQuery(true));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading inbox contact message data!");
            }
        }

        [HttpGet("Outbox")]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> Outbox()
        {
            try
            {
                // false -> Giden Kutusu
                var value = await _getContactInboxOutboxQueryHandler.Handle(new GetContactByMessageTypeQuery(false));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading outbox contact message data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> CreateContact(CreateContactCommand createContactCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createContactCommand);

                if (validator.IsValid)
                {
                    await _createContactCommandHandler.Handle(createContactCommand);

                    return Ok("İletişim Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new contact message data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateContact(UpdateContactCommand updateContactCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateContactCommand);

                if (validator.IsValid)
                {
                    await _updateContactCommandHandler.Handle(updateContactCommand);

                    return Ok("İletişim Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating contact message data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveContact(Guid id)
        {
            try
            {
                await _removeContactCommandHandler.Handle(new RemoveContactCommand(id));

                return Ok("İletişim Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting contact message data!");
            }
        }
    }
}
