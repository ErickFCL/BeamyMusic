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
    public class ArtistasConfiguration : IEntityTypeConfiguration<Artistas>
    {
        public void Configure(EntityTypeBuilder<Artistas> builder)
        {
            builder.ToTable("Artistas");
            builder.HasKey(o => o.Id);

        }
    }
}
