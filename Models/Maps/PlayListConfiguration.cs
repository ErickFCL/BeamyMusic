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
    public class PlayListConfiguration : IEntityTypeConfiguration<PlayList>
    {
        public void Configure(EntityTypeBuilder<PlayList> builder)
        {
            builder.ToTable("PlayList");
            builder.HasKey(o => o.Id);

            //PlayListCancion
            builder.HasMany(o => o.PlayListCanciones).
                WithOne(o => o.PlayListas).
                HasForeignKey(o => o.IdPlayList);
        }
    }
}
