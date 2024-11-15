using personas.services.Application.DTOs;
using personas.services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<PersonaDTO> GetByIdAsync(int id);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<PersonaDTO> AddAsync(PersonaDTO persona);
        Task UpdateAsync(PersonaDTO persona);
        Task DeleteAsync(int id);
    }
}
