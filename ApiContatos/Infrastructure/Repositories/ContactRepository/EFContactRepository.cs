using ApiContatos.Domain;
using ApiContatos.Domain.Enums;
using ApiContatos.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Infrastructure.Repositories.ContactRepository
{
    public class EFContactRepository : IContactRepository
    {

        protected ContactDbContext _context;

        protected DbSet<Contact> _dbset;

        public EFContactRepository(ContactDbContext context)
        {
            _context = context;
            _dbset = context.Set<Contact>();
        }

        public void Create(Contact entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            _dbset.Remove(GetById(id));
            _context.SaveChanges();
        }

        public async Task<(IEnumerable<Contact> data, int totalCount)> GetAll(int page, int pageSize)
        {
            var query = _dbset.AsQueryable();
            var totalCount = await query.CountAsync();

            var paginatedData = await query.Skip((page -1) * pageSize).Take(pageSize).ToListAsync();

            return (paginatedData, totalCount);
        }

        public async Task<(IEnumerable<Contact> data, int totalCount)> GetByDDD(DDD ddd,int page, int pageSize)
        {
        var query = _dbset.Where(c => c.Ddds == ddd);
        var totalCount = await query.CountAsync();
        var paginatedData = await query.Skip((page -1) *pageSize).Take(pageSize).ToListAsync();
        return (paginatedData, totalCount);


        }


        public Contact GetById(long id)
        {
            return _dbset.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Contact entity)
        {
            _dbset.Update(entity);
            _context.SaveChanges();
        }
    }
}
