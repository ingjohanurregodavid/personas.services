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
    public class PersonaService:IPersonaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonaDTO> AddAsync(PersonaDTO personaDTO)
        {
            var persona = new Persona()
            {
                Cedula = personaDTO.Cedula,
                Nombre = personaDTO.Nombre,
                Apellido = personaDTO.Apellido,
                NumeroCelular = personaDTO.NumeroCelular
            };

            await _unitOfWork.PersonaRepository.AddAsync(persona);

            return new PersonaDTO
            {
                Id = persona.Id,
                Cedula = persona.Cedula,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                NumeroCelular=persona.NumeroCelular
            };
        }

        public async Task DeleteAsync(int id)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(id);
            if (persona == null) throw new Exception("Persona no encontrado");

            await _unitOfWork.PersonaRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            //Obtengo el listado de personas y las devuelvo
            var lstPersonas = await _unitOfWork.PersonaRepository.GetAllAsync();
            return lstPersonas.ToList();
        }

        public async Task<PersonaDTO?> GetByIdAsync(int id)
        {
            //Busca a la persona y valida que si exista
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(id);
            if (persona == null)
                throw new KeyNotFoundException($"No se encontró una persona con el Id {id}.");
            //Se mapenan los datos en el DTO
            return new PersonaDTO
            {
                Id = persona.Id,
                Cedula = persona.Cedula,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                NumeroCelular = persona.NumeroCelular
            };
        }

        public async Task UpdateAsync(PersonaDTO personaDTO)
        {
            // Busco a la persona
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(personaDTO.Id);

            // Valido que la persona exista
            if (persona == null)
            {
                throw new KeyNotFoundException($"No se encontró una persona con el Id {personaDTO.Id}.");
            }

            // Actualizo las propiedades
            persona.Cedula = personaDTO.Cedula;
            persona.Nombre = personaDTO.Nombre;
            persona.Apellido = personaDTO.Apellido;
            persona.NumeroCelular = personaDTO.NumeroCelular;

            // Guardo los cambios
            await _unitOfWork.PersonaRepository.UpdateAsync(persona);
            await _unitOfWork.CompleteAsync();
        }
    }
}
