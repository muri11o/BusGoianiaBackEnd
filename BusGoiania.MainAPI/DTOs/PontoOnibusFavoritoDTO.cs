using System.ComponentModel.DataAnnotations;

namespace BusGoiania.MainAPI.DTOs
{
    public class PontoOnibusFavoritoDTO
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NumeroPonto { get; set; }
    }
}
