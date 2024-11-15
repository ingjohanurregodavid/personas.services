
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Domain.Entities
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Cedula { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de celular solo debe contener dígitos.")]
        [StringLength(15, ErrorMessage = "El número de celular no puede exceder los 15 dígitos.")]
        public string NumeroCelular { get; set; }
    }
}
