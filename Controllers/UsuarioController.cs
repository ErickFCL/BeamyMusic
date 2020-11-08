using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BeamyMusic.DataBase;
using BeamyMusic.Models;
using FinancistoCloneWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BeamyMusic.Controllers
{

    [Authorize]
    public class UsuarioController : BaseController
    {
        
        private BeamyContext _context;
        public IHostEnvironment _hostEnv;
        private readonly IConfiguration configuration;

        public UsuarioController(BeamyContext context, IHostEnvironment hostEnv, IConfiguration configuration) : base(context)
        {
            _context = context;
            _hostEnv = hostEnv;
            this.configuration = configuration;
        }
       

        [HttpGet]
        public ActionResult Interface(string search)
        {
            ViewBag.ListPLayMenu = _context.PlayListas.Where(q => q.IdUsuario == LoggedUser().Id).ToList();
            
            ViewData["Message"] = LoggedUser().Nombre;
            ViewBag.ListaPlayList = _context.PlayListas.Where(x => x.Estado == 0).OrderBy(o => o.Nombre).ToList();
            ViewBag.Canciones = _context.Canciones.Include(o => o.Albumes).Include(u => u.Artistas).ToList();
            ViewBag.Buscar = search;
            ViewBag.PlayListas2 = _context.PlayListas.Where(u => u.IdUsuario == LoggedUser().Id);
            var cancion = _context.Canciones
                .Include(o => o.Artistas)
                .Include(y=>y.Albumes)
                .ToList();

            var artista = _context.Artistas
                .ToList();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.PlayListas = _context.PlayListas.Where(u => u.IdUsuario == LoggedUser().Id);
                cancion =  cancion.Where(s => s.Nombre.Contains(search)).ToList();
                ViewBag.cancionPase = cancion;
                artista = artista.Where(s => s.Apellido.Contains(search)).ToList();
                ViewBag.artistaPase = artista;

                return PartialView("Buscar");           
            }

            return PartialView(new Cancion()); 
        }
        [HttpPost]
        public ActionResult Interface(Cancion canciones, IFormFile Cancion, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                // Guardar archivos rn rl servidor:
                if ((Cancion != null && Cancion.Length > 0) && (Foto != null && Foto.Length > 0))
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\Music\" + Cancion.FileName;

                    var basePath1 = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta1 = @"\FtCancion\" + Foto.FileName;
                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        Cancion.CopyTo(strem);
                        canciones.LinkDeCancion = ruta;
                    }
                    using (var strem = new FileStream(basePath1 + ruta1, FileMode.Create))
                    {
                        Foto.CopyTo(strem);
                        canciones.Foto = ruta1;
                    }
                }
                _context.Canciones.Add(canciones);
                _context.SaveChanges();
                return RedirectToAction("Interface"/*lista de canciones*/);
            }
            else
                return View("Interface", canciones);
        }
       
       
        [HttpGet]
        public ActionResult ListaPlayList(int id)
        {
            ViewData["Message"] = LoggedUser().Nombre;
            ViewBag.user2 = _context.PlayListCanciones.Where(o => o.IdPlayList == id)
                .Include(x => x.PlayListas).FirstOrDefault();
            if (ViewBag.user2.PlayListas.IdUsuario == 1)
            {
                ViewBag.User = true;
            }
            else
            {
                ViewBag.User = false;
            }

            ViewBag.PlayListCancion = _context.PlayListCanciones.Where(o => o.IdPlayList == id)
                .Include(x => x.Canciones.Artistas)
                .Include(o => o.Canciones.Albumes)
                .ToList();
            ViewBag.PlayListas = _context.PlayListas.Where(u => u.IdUsuario == LoggedUser().Id);
            ViewBag.PLay = _context.PlayListas.Where(z => z.Id == id).FirstOrDefault();

            return PartialView(new PlayList());
        }
        [HttpGet]
        public ActionResult PlayList()
        {
            ViewData["Message"] = LoggedUser().Nombre;
            ViewBag.PlayListas = _context.PlayListas.Where(o => o.IdUsuario == LoggedUser().Id)
                .ToList();

            return PartialView(new PlayList());
        }
        [HttpPost]
        public ActionResult PlayList(PlayList playList, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                // Guardar archivos rn rl servidor:
                if (Foto != null && Foto.Length > 0)
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\FtPlayList\" + Foto.FileName;
                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        Foto.CopyTo(strem);
                        playList.Foto = ruta;
                    }
                }
                playList.IdUsuario = LoggedUser().Id;
                _context.PlayListas.Add(playList);
                _context.SaveChanges();

                return RedirectToAction("PlayList");
            }
            else
            {
                ViewBag.playList = _context.Usuarios.Where(o => o.Id == LoggedUser().Id)
                .Include(x => x.PlayListas)
                .ToList();
                return View("PlayList", playList);
            }
        }
        [HttpGet]
        public IActionResult AgregarAmiPlayList3(int cancion, int playlist, int playListCan)
        {
            //var playListCancion = _context.PlayListCanciones.FirstOrDefault();
            var listita = new PlayListCancion
            {
                IdPlayList = playlist,
                IdCancion = cancion
            };
            _context.PlayListCanciones.Add(listita);

            _context.SaveChanges();
            return RedirectToAction("ListaPlayList", new { id = playListCan });
        }
        [HttpGet]
        public IActionResult AgregarAmiPlayList(int cancion, int playlist, string _search)
        {
            //var playListCancion = _context.PlayListCanciones.FirstOrDefault();
            var listita = new PlayListCancion
            {
                IdPlayList = playlist,
                IdCancion = cancion
            };
            _context.PlayListCanciones.Add(listita);
            _context.SaveChanges();

            return RedirectToAction("Interface", new { search = _search});
        }
        [HttpGet]
        public IActionResult AgregarAmiPlayList2(int cancion, int playlist)
        {
            //var playListCancion = _context.PlayListCanciones.FirstOrDefault();
            var listita = new PlayListCancion
            {
                IdPlayList = playlist,
                IdCancion = cancion
            };
            _context.PlayListCanciones.Add(listita);
            _context.SaveChanges();

            return RedirectToAction("Favoritos");
        }
        [HttpGet]
        public IActionResult AgregarAmiPlayList4(int cancion, int playlist,int idArtista)
        {
            //var playListCancion = _context.PlayListCanciones.FirstOrDefault();
            var listita = new PlayListCancion
            {
                IdPlayList = playlist,
                IdCancion = cancion
            };
            _context.PlayListCanciones.Add(listita);
            _context.SaveChanges();

            return RedirectToAction("ListaArtista", new { id = idArtista});
        }
        [HttpGet]
        public ActionResult Favoritos()
        {
            ViewData["Message"] = LoggedUser().Nombre;
            ViewBag.favoritos = _context.Favoritos.Where(o => o.IdUsuario == LoggedUser().Id)
                .Include(x => x.Canciones.Artistas)
                .Include(o=>o.Canciones.Albumes).ToList();
            ViewBag.PlayListas = _context.PlayListas.Where(u => u.IdUsuario == LoggedUser().Id);
            return PartialView();
        }
        [HttpGet]
        public ActionResult Favorito(int Cancion,int PlayListActual)
        {
            var favoritos = new Favorito
            {
                IdUsuario = LoggedUser().Id,
                IdCancion = Cancion,
                Fecha = DateTime.Now
            };
             _context.Favoritos.Add(favoritos);
            _context.SaveChanges();

            return RedirectToAction("ListaPlayList", new {id = PlayListActual });
        }
        [HttpGet]
        public ActionResult Favorito3(int idCancion, int idArtista)
        {
            var favoritos = new Favorito
            {
                IdUsuario = LoggedUser().Id,
                IdCancion = idCancion,
                Fecha = DateTime.Now
            };
            _context.Favoritos.Add(favoritos);
            _context.SaveChanges();

            return RedirectToAction("ListaArtista", new { id = idArtista });
        }
        [HttpGet]
        public ActionResult Favorito2(int Cancion, string _search)
        {
            var favoritos = new Favorito
            {
                IdUsuario = LoggedUser().Id,
                IdCancion = Cancion,
                Fecha = DateTime.Now
            };
            _context.Favoritos.Add(favoritos);
            _context.SaveChanges();

            return RedirectToAction("Interface", new { search = _search });
        }
        [HttpGet]
        public ActionResult ListaArtista(int id)
        {
            ViewData["Message"] = LoggedUser().Nombre;
            ViewBag.artista = _context.Canciones.Where(o => o.IdArtista == id).
                Include(x => x.Albumes).
                Include(y => y.Artistas).ToList();
            ViewBag.PlayListas = _context.PlayListas.Where(u => u.IdUsuario == LoggedUser().Id);
            ViewBag.artista2 = _context.Artistas.Where(o => o.Id == id).FirstOrDefault();

            return PartialView();
        }

        [HttpGet]
        public ActionResult Delete(int idcancion,int idPLayList)
        {
            var elimina = _context.PlayListCanciones.Where(o => o.IdCancion == idcancion && o.IdPlayList == idPLayList).FirstOrDefault();
            _context.PlayListCanciones.Remove(elimina);
            _context.SaveChanges();

            return RedirectToAction("ListaPlayList", new {id = idPLayList } ); 
        }
        [HttpGet]
        public ActionResult DeleteF(int idcancion)
        {
            var elimina = _context.Favoritos.Where(o => o.IdCancion == idcancion && o.IdUsuario == LoggedUser().Id).FirstOrDefault();
            _context.Favoritos.Remove(elimina);
            _context.SaveChanges();

            return RedirectToAction("Favoritos");
        }
        public ActionResult Prueba()
        {
            return PartialView();
        }

        //****validaciones de Usuario*******
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
        public void validarUsuarios(Usuario usuario)
        {
            var userv = _context.Usuarios
                .Where(o => o.Nick == usuario.Nick).FirstOrDefault();
            var userCorreo = _context.Usuarios
                .Where(o => o.Correo == usuario.Correo).FirstOrDefault();
            if (userv != null)
            {
                ModelState.AddModelError("usuarioExiste", "Este usuario ya existe");
            }
            if (userCorreo != null)
            {
                ModelState.AddModelError("correoExiste", "Este Correo ya existe");
            }
            if (usuario.Nick.Contains(" ") == true)
            {
                ModelState.AddModelError("NickEspacio", "No se permite espacios en blanco");
            }
            if (!validarLetras(usuario.Nick))
            {
                ModelState.AddModelError("Nickname3", "Solo ingrese caracteres alfabéticos");
            }
            if (usuario.Apellido == null || usuario.Apellido == "")
            {
                ModelState.AddModelError("Apellido", "El campo Apellido es requerido");
            }
            if (usuario.Pass == null || usuario.Pass == "")
            {
                ModelState.AddModelError("Password", "El campo Password es requerido");
            }
            if (usuario.Nombre == null || usuario.Nombre == "")
            {
                ModelState.AddModelError("Nombre", "El campo Nombre es requerido");
            }
            if (!validarLetras(usuario.Nombre))
            {
                ModelState.AddModelError("Nombre1", "Solo ingrese caracteres alfabéticos");
            }
            if (!validarLetras(usuario.Apellido))
            {
                ModelState.AddModelError("Apellido1", "Solo ingrese caracteres alfabéticos");
            }
            if (usuario.Correo == null || usuario.Correo == "")
            {
                ModelState.AddModelError("Correo", "El campo Correo es requerido");
            }
            if (usuario.Nick == null || usuario.Nick == "")
            {
                ModelState.AddModelError("Nickname", "El campo Usuario es requerido");
            }
        }
        public void validarUserLogin(Usuario usuario)
        {

            if (usuario.Nick == null || usuario.Nick == "")
            {
                ModelState.AddModelError("Nickname2", "El campo Nick es requerido");
            }

            if (usuario.Pass == null || usuario.Pass == "")
            {
                ModelState.AddModelError("Password2", "El campo Password es requerido");
            }

        }
        public bool validarLetras(string numString)
        {
            string parte = numString.Trim();
            int count = parte.Count(s => s == ' ');
            if (parte == "")
            {
                return false;
            }
            else if (count > 1)
            {
                return false;
            }
            char[] charArr = parte.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsLetter(cd) && !char.IsSeparator(cd))
                    return false;
            }
            return true;
        }
    }
}