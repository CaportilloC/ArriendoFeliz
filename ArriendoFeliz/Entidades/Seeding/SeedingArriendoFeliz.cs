using Microsoft.EntityFrameworkCore;

namespace ArriendoFeliz.Entidades.Seeding
{
    public static class SeedingArriendoFeliz
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var Toyota = new Marca { MarcaId = 1, NombreMarca = "Toyota" };
            var Chevrolet = new Marca { MarcaId = 2, NombreMarca = "Chevrolet" };
            var Renault = new Marca { MarcaId = 3, NombreMarca = "Renault" };
            var Audi = new Marca { MarcaId = 4, NombreMarca = "Audi" };

            modelBuilder.Entity<Marca>().HasData(Toyota, Chevrolet, Renault, Audi);

            var Logan = new Modelo { ModeloId = 1, NombreModelo = "Logan" };
            var A4 = new Modelo { ModeloId = 2, NombreModelo = "A4" };
            var Fortuner = new Modelo { ModeloId = 3, NombreModelo = "Fortuner" };
            var Spark = new Modelo { ModeloId = 4, NombreModelo = "Spark" };

            modelBuilder.Entity<Modelo>().HasData(Logan, A4, Fortuner, Spark);
        }
    }
}
