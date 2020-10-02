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
    public class CanDeListaDeReproduccionConfiguration : IEntityTypeConfiguration<CanDeListaDeReproduccion>
    {
        public void Configure(EntityTypeBuilder<CanDeListaDeReproduccion> builder)
        {
            builder.ToTable("CanDeListaDeReproduccion");
            builder.HasKey(o => o.Id);

            //Canciones Escuchadas
            builder.HasOne(o => o.CancionesL).
                WithMany(o => o.CanDeListaDeReproduccionL).
                HasForeignKey(o => o.IdCancion);
        }
    }
}
