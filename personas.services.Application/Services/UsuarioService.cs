using personas.services.Application.DTOs;
using personas.services.Application.Interfaces;
using personas.services.Domain.Entities;
using personas.services.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            await _unitOfWork.UsuarioRepository.AddAsync(usuario);

            return usuario;
        }

        public async Task<Usuario> GetUsuario(string login, string password)
        {
            var resul= await _unitOfWork.UsuarioRepository.GetUsuario(login, password);
            return resul;
        }
    }
}
