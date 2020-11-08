using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class PlayList
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public int Estado { get; set; }
        public string Foto { get; set; }
        public List<PlayListCancion> PlayListCanciones { get; set; }
        public Usuario Usuarios { get; set; }
    }
}
