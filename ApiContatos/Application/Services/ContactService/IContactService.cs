using ApiContatos.Domain;
using ApiContatos.Domain.Services;
using System;

namespace ApiContatos.Application.Services.ContactService
{
    public interface IContactService
    {
        Task<(IEnumerable<Contact> data, int totalCount)> GetAllContacts(int page = 1, int pageSize = 10);

        ServiceResult<Contact> GetContactById(long id);

        bool CreateContact(Contact person);

        bool UpdateContact(Contact person);

        ServiceResult<bool> DeleteContact(long id);
    }
}
