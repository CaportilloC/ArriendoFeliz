using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArriendoFeliz.Entidades.Configuraciones
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(prop => prop.Rut)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(prop => prop.Nombres)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(prop => prop.Apellidos)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(prop => prop.Telefono)
                .HasMaxLength(9)
                .IsRequired();

            builder.HasIndex(g => g.Rut).IsUnique().HasFilter("EstaBorrado = 'false'");
        }
    }
}
