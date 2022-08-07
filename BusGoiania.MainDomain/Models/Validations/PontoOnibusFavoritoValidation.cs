using FluentValidation;

namespace BusGoiania.MainDomain.Models.Validations
{
    public class PontoOnibusFavoritoValidation : AbstractValidator<PontoOnibusFavorito>
    {
        public PontoOnibusFavoritoValidation()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Id inválido");

            RuleFor(p => p.UsuarioId)
                .NotEmpty()
                .WithMessage("Id de usuário inválido");

            RuleFor(p => p.NumeroPonto)
                .NotEmpty()
                .NotNull()
                .Length(1, 10)
                .WithMessage("Número de ponto de ônibus inválido");

        }
    }
}
