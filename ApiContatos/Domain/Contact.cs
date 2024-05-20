using ApiContatos.Application.Dto;
using ApiContatos.Domain.Enums;

namespace ApiContatos.Domain
{
    public class Contact
    {
        public Contact()
        {
        }

        public Contact(CreateContactDto createContactDto)
        {
            Name = createContactDto.Name;
            Telephone = createContactDto.Telephone;
            Email = createContactDto.Email;
            Ddds = createContactDto.Ddds;

        }
        public long Id { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public DDD Ddds { get; set; }


    }
}
