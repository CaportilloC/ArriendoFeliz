using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArriendoFeliz.Entidades.Configuraciones
{
    public class MarcaConfig : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.Property(prop => prop.NombreMarca)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(g => g.NombreMarca).IsUnique();
        }
    }
}
