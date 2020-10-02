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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(o => o.Id);

            //Canciones Escuchadas
            builder.HasMany(o => o.CancionesEscuchadasL).
                WithOne(o => o.UsuarioL).
                HasForeignKey(o => o.IdUsuario);

            //Lista De Reproduccion
            builder.HasMany(o => o.ListasDeReproduccionL).
                WithOne(o => o.UsuarioL).
                HasForeignKey(o => o.IdUsuario);
        }
    }
}
