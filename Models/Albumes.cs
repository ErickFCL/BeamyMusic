using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class Albumes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdArtista { get; set; }
        public DateTime FechaDeLanzamiento { get; set; }
        public List<Canciones> CancionesL { get; set; }
        public Artistas ArtistasL { get; set; }
    }
}