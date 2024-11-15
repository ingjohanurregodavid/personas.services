using personas.services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario persona);
        Task<Usuario> GetUsuario(string login, string password);
    }
}
