using ApiContatos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiContatos.Infrastructure.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");
            builder.Property(c => c.Id).HasColumnType("INTEGER").ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(c => c.Telephone).HasColumnType("INT").IsRequired();
            builder.Property(c => c.Email).HasColumnType("VARCHAR(150)").IsRequired();
            builder.Property(c => c.Ddds).HasColumnType("Varchar(150)").IsRequired();
        }
    }
}
