using ApiContatos.Domain;
using ApiContatos.Domain.Enums;
using ApiContatos.Domain.Services;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using System;
using System.Text.RegularExpressions;

namespace ApiContatos.Application.Services.ContactService
{
    public class ContactService : IContactService
    {

        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public bool CreateContact(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Name) || !Regex.IsMatch(contact.Name, @"^[a-zA-ZÀ-ÿ ]+$") || contact.Name.Length > 50)
            {
                return false;
            }

            if (string.IsNullOrEmpty(contact.Telephone) || !Regex.IsMatch(contact.Telephone, @"^[0-9]{8,9}$"))
            {
                return false;
            }

            if (string.IsNullOrEmpty(contact.Email) || !Regex.IsMatch(contact.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") || contact.Email.Length > 100)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(DDD), contact.Ddds))
            {
                return false;
            }
            _contactRepository.Create(contact);
            return true;
        }

        public ServiceResult<bool> DeleteContact(long id)
        {
            var contactToDelete = _contactRepository.GetById(id);
            if (contactToDelete != null)
            {
                _contactRepository.Delete(id);
                return new ServiceResult<bool> { Success = true, Message = "Contato excluído com sucesso" };
            }
            else
            {
                return new ServiceResult<bool> { Success = false, Message = "Contato não encontrada, exclusão não realizada" };
            }
        }

        public Task<(IEnumerable<Contact> data, int totalCount)> GetAllContacts(int page = 1, int pageSize = 10)
        {
            return _contactRepository.GetAll(page, pageSize);
        }

        public ServiceResult<Contact> GetContactById(long id)
        {
            var person = _contactRepository.GetById(id);
            if (person != null)
            {
                return new ServiceResult<Contact> { Success = true, Data = person };
            }
            else
            {
                return new ServiceResult<Contact> { Success = false, Message = "Pessoa não encontrada" };
            }
        }

        public Task<(IEnumerable<Contact> data, int totalcount)> GetContactsByDDD(DDD ddd, int page = 1, int pageSize = 10)
        {
            return _contactRepository.GetByDDD(ddd, page, pageSize);
        }

        public bool UpdateContact(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Name) || !Regex.IsMatch(contact.Name, @"^[a-zA-ZÀ-ÿ ]+$") || contact.Name.Length > 50)
            {
                return false;
            }

            if (string.IsNullOrEmpty(contact.Telephone) || !Regex.IsMatch(contact.Telephone, @"^[0-9]{8,9}$"))
            {
                return false;
            }

            if (string.IsNullOrEmpty(contact.Email) || !Regex.IsMatch(contact.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") || contact.Email.Length > 100)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(DDD), contact.Ddds))
            {
                return false;
            }
            _contactRepository.Update(contact);
            return true;
        }
    }
}
