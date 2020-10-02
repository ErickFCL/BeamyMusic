using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.DataBase.Maps;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeamyMusic.DataBase
{
    public class BeamyContext : DbContext
    {

        public DbSet<Usuario> UsuarioCs { get; set; }
        public DbSet<Canciones> CancionesCs { get; set; }
        public DbSet<CanDeListaDeReproduccion> CanDeListaDeReproduccionCs { get; set; }
        public DbSet<CancionesEscuchadas> CancionesEscuchadasCs { get; set; }
        public DbSet<ListasDeReproduccion> ListasDeReproduccionCs { get; set; }
        public DbSet<Albumes> AlbumesCs { get; set; }
        public DbSet<Artistas> ArtistasCs { get; set; }
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HP-PORTATIL-240\\SQLEXPRES; DataBase=BeamyMusic;Trusted_Connection=True;MultipleActiveResultSets=true");
        }*/
        public BeamyContext(DbContextOptions<BeamyContext> options) : base(options)
        {


        }

        public BeamyContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new CancionesConfiguration());
            modelBuilder.ApplyConfiguration(new CanDeListaDeReproduccionConfiguration());
            modelBuilder.ApplyConfiguration(new CancionesEscuchadasConfiguration());
            modelBuilder.ApplyConfiguration(new ListasDeReproduccionConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumesConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistasConfiguration());
            
        }
    }
}
