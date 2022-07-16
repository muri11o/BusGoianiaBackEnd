using BusGoiania.AuthProvider.Data.Conta;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusGoiania.AuthProvider.Data
{
    public class IdentityDbContext : IdentityDbContext<Usuario>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }
}
