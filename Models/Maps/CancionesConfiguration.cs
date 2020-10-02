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
    public class CancionesConfiguration : IEntityTypeConfiguration<Canciones>
    {
        public void Configure(EntityTypeBuilder<Canciones> builder)
        {
            builder.ToTable("Canciones");
            builder.HasKey(o => o.Id);

            //Albumes
            builder.HasOne(o => o.AlbumesL).
                WithMany(o => o.CancionesL).
                HasForeignKey(o => o.IdAlbum);
        }
    }
}
