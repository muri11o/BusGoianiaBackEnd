using BusGoiania.AuthProvider.Data.Conta;
using BusGoiania.AuthProvider.DTOs;
using BusGoiania.AuthProvider.Interfaces;
using BusGoiania.AuthProvider.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusGoiania.AuthProvider.Controllers
{
    [Route("v1/api/conta")]
    public class ContaController : MainController
    {
        private readonly UserManager<Usuario> _userManager;
        public ContaController(UserManager<Usuario> userManager, INotifier notifier) : base(notifier)
        {
            _userManager = userManager;
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> Cadastrar(CadastroUsuarioDTO cadastroUsuarioDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var user = new Usuario
                {
                    UserName = cadastroUsuarioDTO.Email,
                    Email = cadastroUsuarioDTO.Email,
                    Nome = cadastroUsuarioDTO.Nome
                };

                var result = await this._userManager.CreateAsync(user, cadastroUsuarioDTO.Senha);

                if (result.Succeeded)
                    return CustomResponse(new object[] { new Notification("Conta cadastrada com sucesso") });

                foreach (var error in result.Errors)
                    NotifyError(error.Description);


                return CustomResponse(cadastroUsuarioDTO);
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }
    }
}