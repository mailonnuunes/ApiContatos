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

        public DbSet<Contact> contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

              
                optionsBuilder.UseNpgsql(_configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
            
        }
    }
}
