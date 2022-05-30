using Microsoft.AspNetCore.Identity;

namespace BusGoiania.AuthProvider.Data.Conta
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
    }
}
