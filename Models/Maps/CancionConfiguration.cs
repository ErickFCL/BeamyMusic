using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeamyMusic.DataBase.Maps
{
    public class CancionConfiguration : IEntityTypeConfiguration<Cancion>
    {
        public void Configure(EntityTypeBuilder<Cancion> builder)
        {
            builder.ToTable("Cancion");
            builder.HasKey(o => o.Id);

            //Albumes
            builder.HasOne(o => o.Albumes).
                WithMany(o => o.Canciones).
                HasForeignKey(o => o.IdAlbum);

            //Artistas
            builder.HasOne(o => o.Artistas).
                WithMany(o => o.Canciones).
                HasForeignKey(o => o.IdArtista);

            //
            //PlayListCancion
            builder.HasMany(o => o.PlayListCanciones).
                WithOne(o => o.Canciones).
                HasForeignKey(o => o.IdPlayList);
        }
    }
}
