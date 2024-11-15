using personas.services.Infrastructure.Data;
using personas.services.Infrastructure.Repositories;
using personas.services.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Infrastructure.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly PersonaDbContext _context;
        private IPersonaRepository _personaRepository;
        private IUsuarioRepository _usuarioRepository;
        public UnitOfWork(PersonaDbContext context)
        {
            _context = context;
        }

        public IPersonaRepository PersonaRepository => _personaRepository ??= new PersonaRepository(_context);
        public IUsuarioRepository UsuarioRepository => _usuarioRepository ??= new UsuarioRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
