using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArriendoFeliz.Entidades
{
    public class Arriendo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public int ModeloId { get; set; }
        [ForeignKey("ModeloId")]
        public Modelo Modelo { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public DateTime? FechaInicio { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        [StringLength(20, ErrorMessage = "Debe tener 20 caracteres")]
        [Remote("PatenteDisponible", "Arriendos", AdditionalFields = "Id, Patente", ErrorMessage = "Ya existe un carro con esta patente.")]
        public string Patente { get; set; }
        public bool EstaBorrado { get; set; }

        [NotMapped]
        public int? DiasArriendo
        {
            get
            {
                if (!FechaInicio.HasValue || !FechaFin.HasValue)
                {
                    return null;
                }

                if (FechaFin.Value < FechaInicio.Value)
                {
                    return null;
                }

                var diasArriendo = (int)(FechaFin.Value - FechaInicio.Value).TotalDays;

                return diasArriendo;
            }
        }
    }
}
