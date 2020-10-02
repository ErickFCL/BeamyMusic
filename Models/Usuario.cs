using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateTime FecDeCreacion { get; set; }
        [Required]
        public string Pass { get; set; }
        [Required]
        public string Nick { get; set; }
        public string Imagen { get; set; }
        public List<CancionesEscuchadas> CancionesEscuchadasL { get; set; }
       public List<ListasDeReproduccion> ListasDeReproduccionL { get; set; }
    }
}
