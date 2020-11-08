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

            //Favoritos
            builder.HasMany(o => o.Favoritos).
                WithOne(o => o.Usuarios).
                HasForeignKey(o => o.IdUsuario);

            //PlayList
            builder.HasMany(o => o.PlayListas).
                WithOne(o => o.Usuarios).
                HasForeignKey(o => o.IdUsuario);
        }
    }
}
