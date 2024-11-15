using personas.services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Infrastructure.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> GetByIdAsync(int id);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task AddAsync(Persona persona);
        Task UpdateAsync(Persona persona);
        Task DeleteAsync(int id);
    }
}
