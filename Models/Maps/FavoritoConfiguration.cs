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
    public class FavoritoConfiguration : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> builder)
        {
            builder.ToTable("Favorito");
            builder.HasKey(o => o.Id);

            //Canciones
            builder.HasOne(o => o.Canciones).
                WithMany(o => o.Favoritos).
                HasForeignKey(o => o.IdCancion);
        }
    }
}
