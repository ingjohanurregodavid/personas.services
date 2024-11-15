using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Domain.Entities
{
    public class Usuario
    {
        [Key]  
        public int Id { get; set; }      

        [Required]                           
        [MaxLength(100)]                     
        public string? Login { get; set; }     

        [Required]                           
        [MinLength(6)]                       
        public string? Password { get; set; }
    }
}
