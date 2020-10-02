using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class CanDeListaDeReproduccion
    {
        public int Id { get; set; }
        public int IdLisReproduccion { get; set; }
        public int IdCancion { get; set; }
        public ListasDeReproduccion listasDeReproduccionL { get; set; }
        public Canciones CancionesL { get; set; }
    }
}