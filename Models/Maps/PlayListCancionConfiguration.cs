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
    public class PlayListCancionConfiguration : IEntityTypeConfiguration<PlayListCancion>
    {
        public void Configure(EntityTypeBuilder<PlayListCancion> builder)
        {
            builder.ToTable("PlayListCancion");
            builder.HasKey(o => o.Id);

            //canciones
            builder.HasOne(o => o.Canciones).
                WithMany(o => o.PlayListCanciones).
                HasForeignKey(o => o.IdCancion);

            //playlist
           
            builder.HasOne(o => o.PlayListas).
                WithMany(o => o.PlayListCanciones).
                HasForeignKey(o => o.IdPlayList);
        }
    }
}
