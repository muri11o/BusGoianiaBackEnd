using BusGoiania.MainDomain.Interfaces;
using BusGoiania.MainDomain.Models;
using BusGoiania.MainDomain.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace BusGoiania.MainDomain.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notificador;

        public BaseService(INotifier notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notification(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (!validator.IsValid)
            {
                Notificar(validator);
                return false;
            }

            return true;
        }
    }
}
