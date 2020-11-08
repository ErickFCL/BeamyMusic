using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class Cancion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Duracion { get; set; }
        public string LinkDeCancion { get; set; }
        public int IdAlbum { get; set; }
        public int IdArtista { get; set; }
        public string Foto { get; set; }
        public List<Favorito> Favoritos { get; set; }
        public List<PlayListCancion> PlayListCanciones { get; set; }
        public Album Albumes { get; set; }
        public Artista Artistas { get; set; }
    }
}