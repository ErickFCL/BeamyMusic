using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.DataBase.Maps;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyBeamyMusic.Models;

namespace BeamyMusic.DataBase
{
    public class BeamyContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cancion> Canciones { get; set; }
        public DbSet<PlayListCancion> PlayListCanciones { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<PlayList> PlayListas { get; set; }
        public DbSet<Album> Albumes { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<DetalleSeguir> DetalleSeguir { get; set; }
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
            modelBuilder.ApplyConfiguration(new CancionConfiguration());
            modelBuilder.ApplyConfiguration(new PlayListCancionConfiguration());
            modelBuilder.ApplyConfiguration(new FavoritoConfiguration());
            modelBuilder.ApplyConfiguration(new PlayListConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistaConfiguration());
            modelBuilder.ApplyConfiguration(new DetalleSeguirConfiguration());
            
        }
    }
}
