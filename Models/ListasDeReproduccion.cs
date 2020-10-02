using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class ListasDeReproduccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public List<CanDeListaDeReproduccion> CanDeListaDeReproduccionL { get; set; }
        public Usuario UsuarioL { get; set; }
    }
}
