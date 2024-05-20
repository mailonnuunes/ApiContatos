using ApiContatos.Domain.Enums;

namespace ApiContatos.Application.Dto
{
    public class CreateContactDto
    {
        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public DDD Ddds { get; set; }
    }
}
