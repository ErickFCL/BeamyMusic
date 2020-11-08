using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaDeLanzamiento { get; set; }
        public string Foto { get; set; }
        public List<Cancion> Canciones { get; set; }
    }
}