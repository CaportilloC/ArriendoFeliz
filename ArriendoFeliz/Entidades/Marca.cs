using System.ComponentModel.DataAnnotations;

namespace ArriendoFeliz.Entidades
{
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }
        public string NombreMarca { get; set; }
        public HashSet<Arriendo> Arriendos { get; set; }
    }
}
