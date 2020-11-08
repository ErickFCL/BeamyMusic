using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class PlayListCancion
    {
        public int Id { get; set; }
        public int IdPlayList { get; set; }
        public int IdCancion { get; set; }
        public PlayList PlayListas { get; set; }
        public Cancion Canciones { get; set; }
    }
}