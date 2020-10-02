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
    public class AlbumesConfiguration : IEntityTypeConfiguration<Albumes>
    {
        public void Configure(EntityTypeBuilder<Albumes> builder)
        {
            builder.ToTable("Albumes");
            builder.HasKey(o => o.Id);

            //Artistas
            builder.HasOne(o => o.ArtistasL).
                WithMany(o => o.AlbumesL).
                HasForeignKey(o => o.IdArtista);
        }
    }
}
