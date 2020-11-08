using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class AdminController : BaseController
    {
        private BeamyContext _context;
        public IHostEnvironment _hostEnv;
        private readonly IConfiguration configuration;


        public AdminController(BeamyContext context, IHostEnvironment hostEnv, IConfiguration configuration) : base(context)
        {
            _context = context;
            _hostEnv = hostEnv;
            this.configuration = configuration;
        }

       
        [HttpGet]
        public ActionResult Index()
        {
            var canciones = _context.Canciones
                 .Include(o => o.Artistas)
                 .Include(x=>x.Albumes)
                 .ToList();
            ViewBag.Artista = _context.Artistas.ToList();
            ViewBag.Album = _context.Albumes.ToList();
            return View(new Cancion());
        }
        [HttpPost]
        public ActionResult Index(Cancion canciones, IFormFile Cancion, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                // Guardar archivos rn rl servidor:
                if ((Cancion != null && Cancion.Length > 0)&& (Foto != null && Foto.Length > 0))
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
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Artista = _context.Artistas.ToList();
                ViewBag.Album = _context.Albumes.ToList();
                return View("Index", canciones);
            }
        }
               [Route("Exito")]
        public IActionResult Exito()
        {

            return View();
        }
        [HttpGet]
        public ActionResult CrearArtista()
        {
            return View(new Artista());
        }
        [HttpPost]
        public ActionResult CrearArtista(Artista artistas, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                // Guardar archivos rn rl servidor:
                if (Foto != null && Foto.Length > 0)
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\FtArtista\" + Foto.FileName;
                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        Foto.CopyTo(strem);
                        artistas.Foto = ruta;
                    }
                }
                _context.Artistas.Add(artistas);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
               
                return View("CrearArtista", artistas);
            }
        }
        [HttpGet]
        public ActionResult CrearAlbum()
        {
            return View(new Album());
        }
        [HttpPost]
        public ActionResult CrearAlbum(Album albumes, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                // Guardar archivos rn rl servidor:
                if (Foto != null && Foto.Length > 0)
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\FtAlbum\" + Foto.FileName;
                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        Foto.CopyTo(strem);
                        albumes.Foto = ruta;
                    }
                }
                _context.Albumes.Add(albumes);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Artista = _context.Artistas.ToList();
                ViewBag.Album = _context.Albumes.ToList();
                return View("CrearAlbum", albumes);
            }
        }
        public ActionResult CrearPlayList()
        {
           // var account = new Account();
          
           
            ViewBag.canciones = _context.Canciones.Include(o => o.PlayListCanciones)
                .Include(x => x.Favoritos).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult PlayList()
        {
            ViewBag.playList = _context.PlayListas
                .ToList();
            
            return View(new PlayList());
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
                return RedirectToAction("CrearPlayList");

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
        public IActionResult AgregarPorDefecto(int Id)
        {
          
            ViewBag.paseId = Id;
            ViewBag.playList = _context.PlayListas.ToList();
            return View();
        }
     
        [HttpGet]
        public IActionResult AgregarPorDefecto2(int cancion, int playlist)
        {
            //var playListCancion = _context.PlayListCanciones.FirstOrDefault();
            var listita = new PlayListCancion
            { 
                IdPlayList = playlist, 
                IdCancion = cancion
            };
            _context.PlayListCanciones.Add(listita);
      
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
