using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArriendoFeliz.Entidades.Configuraciones
{
    public class ArriendoConfig : IEntityTypeConfiguration<Arriendo>
    {
        public void Configure(EntityTypeBuilder<Arriendo> builder)
        {
            builder.HasIndex(g => g.Patente).IsUnique().HasFilter("EstaBorrado = 'false'");
        }
    }
}
