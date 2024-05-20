using ApiContatos.Application.Dto;
using ApiContatos.Application.Services.ContactService;
using ApiContatos.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiContatos.Presentation.Controllers
{
    [ApiController]
    [Route("Contatos")]
    public class ContactController : ControllerBase
    {

        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("obter-todos-os-contatos")]
        public async Task<IActionResult> GetAllContacts(int page = 1, int pageSize = 10)
        {
           

            var (contacts, totalCount) = await _contactService.GetAllContacts(page, pageSize);

            var response = new
            {
                Data = contacts,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = page
            };

            return Ok(response);
        }
        [HttpGet("Obter-contato-por-id")]
        public IActionResult GetContactById(int id)
        {
           
            var result = _contactService.GetContactById(id);

            if (result.Success)
            {
                return Ok(result.Data); // Retorna 200 OK com os dados da pessoa
            }
            else
            {
                return NotFound(result.Message); // Retorna 404 Not Found com a mensagem de erro
            }
        }
        [HttpPost("Criar-contato")]
        public IActionResult CreateContact(CreateContactDto createPersonDto)
        {

            var person = new Contact(createPersonDto);
            _contactService.CreateContact(person);
            var newPersonId = person.Id;
           
            return CreatedAtAction(nameof(GetContactById), new { id = newPersonId }, person);
        }


        [HttpPut("Atualizar-contato")]
        public IActionResult UpdateContact(Contact person)
        {
            _contactService.UpdateContact(person);
            return NoContent();
        }
        [HttpDelete("Deletar-pessoa")]
        public IActionResult DeletePerson(int id)
        {

            var result = _contactService.DeleteContact(id);

            if (result.Success)
            {
                return NoContent(); // Retorna 200 OK com a mensagem de sucesso
            }
            else
            {
                return NotFound(result.Message); // Retorna 404 Not Found com a mensagem de erro
            }
        }
        }

}

