using ApiContatos.Application.Dto;
using ApiContatos.Application.Services.ContactService;
using ApiContatos.Domain;
using ApiContatos.Domain.Enums;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text;

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
        [HttpGet("Obter-Contatos-por-DDD")]
        public async Task<IActionResult> GetContactsByDDD(DDD ddds,int page = 1, int pageSize = 10)
        {

            var (contacts, totalCount) = await _contactService.GetContactsByDDD(ddds,page, pageSize);

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
            var factory = new ConnectionFactory()
            { HostName = "localhost", UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "fila-criar-contato",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer
                    .Serialize(person);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "fila-criar-contato",
                    basicProperties: null,
                    body: body);
            }

            return Ok();

        }


        [HttpPut("Atualizar-contato")]
        public IActionResult UpdateContact(Contact person)
        {
            
            var factory = new ConnectionFactory()
            { HostName = "localhost", UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "fila-atualizar-contato",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer
                    .Serialize(person);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "fila-atualizar-contato",
                    basicProperties: null,
                    body: body);
            }

            return NoContent();
        }
        [HttpDelete("Deletar-pessoa")]
        public IActionResult DeletePerson(int id)
        {

            var result = _contactService.DeleteContact(id);

            if (result.Success)
            {
                var factory = new ConnectionFactory()
                { HostName = "localhost", UserName = "guest", Password = "guest" };
                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "fila-deletar-contato",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = JsonSerializer
                        .Serialize(id);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "fila-deletar-contato",
                        basicProperties: null,
                        body: body);
                }

                return NoContent();
            }
            else
            {
                return NotFound(result.Message); // Retorna 404 Not Found com a mensagem de erro
            }
        }
        }

}

