using System.ComponentModel.DataAnnotations;

namespace ArriendoFeliz.Entidades
{
    public class Modelo
    {
        public int ModeloId { get; set; }
        public string NombreModelo { get; set; }
        public HashSet<Arriendo> Arriendos { get; set; }
    }
}
