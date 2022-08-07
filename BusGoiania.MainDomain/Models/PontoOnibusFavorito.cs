namespace BusGoiania.MainDomain.Models
{
    public class PontoOnibusFavorito : Entity
    {
        public Guid UsuarioId { get; set; }
        public string NumeroPonto { get; set; }
    }
}
