using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArriendoFeliz.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(10, ErrorMessage = "Debe tener 10 caracteres")]
        [Remote("RutDisponible", "Clientes", AdditionalFields = "Id, Rut", ErrorMessage = "Ya existe una persona con este numero de rut.")]
        [RegularExpression(@"^\d{8}-[0-9Kk]$", ErrorMessage = "Formato incorrecto. Debe ser 99999999-9")]
        public string Rut { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(25, ErrorMessage = "Debe tener 25 caracteres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(25, ErrorMessage = "Debe tener 25 caracteres")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(9, ErrorMessage = "Debe tener 9 caracteres")]
        public string Telefono { get; set; }
        public bool EstaBorrado { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ValidarDigitoVerificador(Rut))
            {
                yield return new ValidationResult("Dígito verificador inválido", new[] { nameof(Rut) });
            }
        }

        private bool ValidarDigitoVerificador(string rut)
        {
            throw new NotImplementedException("Implementa la validación del dígito verificador");
        }
    }
}
