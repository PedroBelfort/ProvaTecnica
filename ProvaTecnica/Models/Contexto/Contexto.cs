using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProvaTecnica.Models;

namespace ProvaTecnica.Models.Contexto
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerfilFuncionalidade>()
                .HasKey(t => new { t.Id });

            modelBuilder.Entity<PerfilFuncionalidade>()
                .HasOne(pt => pt.Perfil)
                .WithMany(p => p.PerfilFuncionalidades)
                .HasForeignKey(pt => pt.PerfilId);

            modelBuilder.Entity<PerfilFuncionalidade>()
                .HasOne(pt => pt.Funcionalidade)
                .WithMany(t => t.PerfilFuncionalidades)
                .HasForeignKey(pt => pt.FuncionalidadeId);
        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Funcionalidade> Funcionalidades  { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<ProvaTecnica.Models.PerfilFuncionalidade> PerfilFuncionalidade { get; set; }

      
    }
}
