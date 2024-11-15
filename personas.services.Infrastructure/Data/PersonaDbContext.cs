using Microsoft.EntityFrameworkCore;
using personas.services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas.services.Infrastructure.Data
{
   
    public class PersonaDbContext:DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public PersonaDbContext(DbContextOptions<PersonaDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
               .HasIndex(p => p.Cedula)
               .IsUnique();
            modelBuilder.Entity<Usuario>()
              .HasIndex(p => p.Login)
              .IsUnique();
        }
    }
}
