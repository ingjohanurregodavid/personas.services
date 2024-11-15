using personas.services.Application.DTOs;
using personas.services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> GetUsuario(string login, string password);
    }
}
