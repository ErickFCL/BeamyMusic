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
        [Required]
        [MinLength(3)]
        public string Nombre { get; set; }
        [Required]
        [MinLength(3)]
        public string Apellido { get; set; }
        [Required]
        //[Unique(typeof(Correo))]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail no es válido")]
        public string Correo { get; set; }
        [Required]
        public DateTime FecDeCreacion { get; set; }
        [Required]
        [MinLength (6)]
        [MaxLength (8)]
        public string Pass { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(8)]
        
        public string Nick { get; set; }
        public string Imagen { get; set; }
        public List<Favorito> Favoritos { get; set; }
       public List<PlayList> PlayListas { get; set; }
    }
}
