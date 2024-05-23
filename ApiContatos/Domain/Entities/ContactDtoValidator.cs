using ApiContatos.Application.Dto;
using ApiContatos.Domain.Enums;
using FluentValidation;

namespace ApiContatos.Domain.Entities
{
    public class ContactDtoValidator : AbstractValidator<CreateContactDto>
    {

        public ContactDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo 'nome' é obrigatório.")
                .Matches("^[a-zA-ZÀ-ÿ ]+$").WithMessage("O campo 'nome' deve conter apenas letras e espaços.")
                .MaximumLength(50).WithMessage("O campo 'nome' deve ter no máximo 50 caracteres.");
            RuleFor(c => c.Telephone)
                .NotEmpty()
                .Length(8, 9).WithMessage("O campo 'telefone' deve ter entre 8 e 9 caracteres.")
                .Matches("^[0-9]+$").WithMessage("O campo 'telefone' deve conter apenas números.");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo 'email' é obrigatório.")
                .EmailAddress().WithMessage("O campo 'email' deve conter um endereço de email válido.")
                .MaximumLength(100).WithMessage("O campo 'email' deve ter no máximo 150 caracteres.");
            RuleFor(c => c.Ddds)
                .NotEmpty().WithMessage("O campo 'DDD' é obrigatório.")
                .Must(d => Enum.IsDefined(typeof(DDD), d)).WithMessage("O campo 'DDD' deve conter um código DDD válido.");
        }
    }
}
