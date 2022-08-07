using BusGoiania.MainDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusGoiania.MainData.Mappings
{
    internal class PontoOnibusFavoritoMapping : IEntityTypeConfiguration<PontoOnibusFavorito>
    {
        public void Configure(EntityTypeBuilder<PontoOnibusFavorito> builder)
        {
            builder.ToTable("pontosOnibusFavoritos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UsuarioId)
                .IsRequired();

            builder.Property(f => f.NumeroPonto)
                .IsRequired()
                .HasColumnType("VARCHAR(10)");
        }
    }
}
