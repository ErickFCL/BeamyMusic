using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class Canciones
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Duracion { get; set; }
        public string LinkDeCancion { get; set; }
        public int IdAlbum { get; set; }
        public List<CancionesEscuchadas> CancionesEscuchadasL { get; set; }
        public List<CanDeListaDeReproduccion> CanDeListaDeReproduccionL { get; set; }
        public Albumes AlbumesL { get; set; }
    }
}