using ApiContatos.Domain;

namespace ApiContatos.Infrastructure.Repositories.ContactRepository
{
    public interface IContactRepository 
    {
        Task<(IEnumerable<Contact> data, int totalCount)> GetAll(int page, int pageSize);

        Contact GetById(long id);

        void Create(Contact entity);

        void Update(Contact entity);

        void Delete(long id);



    }
}
