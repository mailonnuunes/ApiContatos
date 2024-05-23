using ApiContatos.Application.Services.ContactService;
using ApiContatos.Domain;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContatosTestes.Application.Services
{
    public class ContactServiceTests
    {
        private readonly ContactService _contactService;

        private readonly Mock<IContactRepository> _contactRepositoryMock;

        private readonly Mock<IValidator<Contact>> _validatorMock;

        public ContactServiceTests()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactService = new ContactService(_contactRepositoryMock.Object);
            _validatorMock = new Mock<IValidator<Contact>>();
        }

        [Fact]
        public void POST_SendingInvalidUserToCreation()
        {

            var contactData = new Contact()
            {
                Name = "F!bric1o",
                Email = "fabriciolima121@gmail.com",
                Telephone = "1241241",
                Ddds = (ApiContatos.Domain.Enums.DDD)271
            };

            var createContact = _contactService.CreateContact(contactData);

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
        [Fact]
        public void PUT_EditingPersonCorrectly()
        {
            // Arrange
            var peopleList = new List<Contact>
            {
                new Contact { Id = 1, Name = "Mailon", Email = "batman@gotham.com", Telephone = "992923680", Ddds = (ApiContatos.Domain.Enums.DDD)11 },
                new Contact { Id = 2, Name = "Cadu", Email = "supertrinit@gotham.com", Telephone = "992923680", Ddds = (ApiContatos.Domain.Enums.DDD)11 }
            };

            _contactRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                                  .Returns((int id) => peopleList.FirstOrDefault(p => p.Id == id));

            _contactRepositoryMock.Setup(repo => repo.Update(It.IsAny<Contact>()))
                                  .Callback<Contact>(updatedContact =>
                                  {
                                      var contact = peopleList.FirstOrDefault(c => c.Id == updatedContact.Id);
                                      if (contact != null)
                                      {
                                          contact.Name = updatedContact.Name;
                                          contact.Email = updatedContact.Email;
                                          contact.Telephone = updatedContact.Telephone;
                                          contact.Ddds = updatedContact.Ddds;
                                      }
                                  });

            _validatorMock.Setup(v => v.Validate(It.IsAny<Contact>())).Returns(new ValidationResult());

            var personToUpdate = new Contact
            {
                Id = 1, // ID que existe na lista
                Name = "Eduardo",
                Email = "mailonnunes2016@gmail.com",
                Telephone = "992923680",
                Ddds = (ApiContatos.Domain.Enums.DDD)11
            };

            // Act
            var result = _contactService.UpdateContact(personToUpdate);

            // Assert
            Assert.True(result);

            // Verificar se o contato foi atualizado corretamente na lista
            var updatedContact = peopleList.FirstOrDefault(c => c.Id == 1);
            Assert.NotNull(updatedContact);
            Assert.Equal("Eduardo", updatedContact.Name);
            Assert.Equal("mailonnunes2016@gmail.com", updatedContact.Email);
            Assert.Equal("992923680", updatedContact.Telephone);
            Assert.Equal((ApiContatos.Domain.Enums.DDD)11, updatedContact.Ddds);
        }
        [Fact]
        public void PUT_EditingPersonIncorrectly()
        {
            // Arrange
            var peopleList = new List<Contact>
            {
                new Contact { Id = 1, Name = "Mailon", Email = "batman@gotham.com", Telephone = "945255971", Ddds = (ApiContatos.Domain.Enums.DDD)11 },
                new Contact { Id = 2, Name = "Cadu", Email = "supertrinit@gotham.com", Telephone = "991325672", Ddds = (ApiContatos.Domain.Enums.DDD)11 }
            };

            _contactRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                                  .Returns((int id) => peopleList.FirstOrDefault(p => p.Id == id));

            _contactRepositoryMock.Setup(repo => repo.Update(It.IsAny<Contact>()))
                                  .Callback<Contact>(updatedContact =>
                                  {
                                      var contact = peopleList.FirstOrDefault(c => c.Id == updatedContact.Id);
                                      if (contact != null)
                                      {
                                          contact.Name = updatedContact.Name;
                                          contact.Email = updatedContact.Email;
                                          contact.Telephone = updatedContact.Telephone;
                                          contact.Ddds = updatedContact.Ddds;
                                      }
                                  });

            _validatorMock.Setup(v => v.Validate(It.IsAny<Contact>())).Returns(new ValidationResult());

            var personToUpdate = new Contact
            {
                Id = 1, // ID que existe na lista
                Name = "Edu1rdo",
                Email = "mailonnunes2016@gmail.com",
                Telephone = "87993823680",
                Ddds = (ApiContatos.Domain.Enums.DDD)11
            };

            // Act
            var result = _contactService.UpdateContact(personToUpdate);

            // Assert
            Assert.False(result);
        }
    }
    }


