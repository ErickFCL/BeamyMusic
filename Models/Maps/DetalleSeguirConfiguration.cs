using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyBeamyMusic.Models;

namespace BeamyMusic.DataBase.Maps
{
    public class DetalleSeguirConfiguration : IEntityTypeConfiguration<DetalleSeguir>
    {
        public void Configure(EntityTypeBuilder<DetalleSeguir> builder)
        {
            builder.ToTable("DetalleSeguir");
            builder.HasKey(o => o.Id);

      
            builder.HasOne(o => o.Usuarios).
                WithMany().
                HasForeignKey(o => o.IdAmigo);
        }
    }
}
