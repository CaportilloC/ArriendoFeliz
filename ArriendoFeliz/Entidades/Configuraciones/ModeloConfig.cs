using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArriendoFeliz.Entidades.Configuraciones
{
    public class ModeloConfig : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.Property(prop => prop.NombreModelo)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(g => g.NombreModelo).IsUnique();
        }
    }
}
