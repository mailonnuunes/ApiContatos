using ApiContatos.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Infrastructure.Data.DbContexts
{
    public class ContactDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ContactDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
           : base(options)
        {
        }
        public DbSet<Contact> contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration != null)
            {
              
                optionsBuilder.UseSqlite(_configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
            }
        }
    }
}
