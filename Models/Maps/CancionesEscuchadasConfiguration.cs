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
    public class CancionesEscuchadasConfiguration : IEntityTypeConfiguration<CancionesEscuchadas>
    {
        public void Configure(EntityTypeBuilder<CancionesEscuchadas> builder)
        {
            builder.ToTable("CancionesEscuchadas");
            builder.HasKey(o => o.Id);

            //Canciones
            builder.HasOne(o => o.CancionesL).
                WithMany(o => o.CancionesEscuchadasL).
                HasForeignKey(o => o.IdCancion);
        }
    }
}
