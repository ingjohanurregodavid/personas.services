
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Application.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroCelular { get; set; }

        public PersonaDTO()
        {
            
        }
    }
}
