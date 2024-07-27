using ApiContatos.Application.Services.ContactService;
using ApiContatos.Domain;
using ApiContatos.Infrastructure.Data.DbContexts;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContatosTestes.Integration
{
    public class ContactServiceIntegrationTests
    {

        private readonly ContactDbContext _context;
        private readonly ContactService _contactService;

        public ContactServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ContactDbContext(options);
            _contactService = new ContactService(new EFContactRepository(_context));
        }

        [Fact]
        public void POST_SendingInvalidUserToCreation()
        {
            // Arrange
            var contactData = new Contact()
            {
                Name = "F!bric1o",
                Email = "fabriciolima121@gmail.com",
                Telephone = "1241241",
                Ddds = (ApiContatos.Domain.Enums.DDD)271
            };

            // Act
            var createContact = _contactService.CreateContact(contactData);

            // Assert
            Assert.False(createContact);
        }
        [Fact]
        public void POST_SendingValidUserToCreation()
        {

            var contactData = new Contact()
            {
                Name = "Mailon",
                Email = "mailonnunes2016@gmail.com",
                Telephone = "956823680",
                Ddds = (ApiContatos.Domain.Enums.DDD)11
            };

            var createContact = _contactService.CreateContact(contactData);

            Assert.True(createContact);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
