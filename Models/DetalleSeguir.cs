using BeamyMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyBeamyMusic.Models
{
    public class DetalleSeguir
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdAmigo { get; set; }
        public DateTime FechaSeguir { get; set; }
        //
        public Usuario Usuarios { get; set; }
    }
}
