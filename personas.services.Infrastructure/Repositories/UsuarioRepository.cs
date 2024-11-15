using Microsoft.EntityFrameworkCore;
using personas.services.Domain.Entities;
using personas.services.Infrastructure.Data;
using personas.services.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PersonaDbContext _context;

        public UsuarioRepository(PersonaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Usuario persona)
        {
            await _context.Usuarios.AddAsync(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> GetUsuario(string login, string password)
        {

            var rest= await _context.Usuarios
                        .Where(u => u.Login == login && u.Password == password)
                        .FirstOrDefaultAsync();
            return rest;
        }
    }
}
