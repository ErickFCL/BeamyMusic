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
    public class ListasDeReproduccionConfiguration : IEntityTypeConfiguration<ListasDeReproduccion>
    {
        public void Configure(EntityTypeBuilder<ListasDeReproduccion> builder)
        {
            builder.ToTable("ListasDeReproduccion");
            builder.HasKey(o => o.Id);

            //Can De Lista De Reproduccion
            builder.HasMany(o => o.CanDeListaDeReproduccionL).
                WithOne(o => o.listasDeReproduccionL).
                HasForeignKey(o => o.IdLisReproduccion);
        }
    }
}
