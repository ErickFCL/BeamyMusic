using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BeamyMusic.DataBase;
using BeamyMusic.Models;
using FinancistoCloneWeb.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BeamyMusic.Controllers
{
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
        public ActionResult Index()
        {
            var usuario = new Usuario();
            var usuarios = _context.UsuarioCs
                .Where(o => o.Id == LoggedUser().Id)
                .Include(o => o.CancionesEscuchadasL)
                .Include(o => o.ListasDeReproduccionL)
                .ToList();

            return View(usuarios);
        }
        [HttpGet]
       public ActionResult Registrar()
        {
           
            return View("Registrar",new Usuario());
        }
        //[Route("Registrar")]
        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            try
            {
                validarUsuarios(usuario);
                if (ModelState.IsValid)
                {
                var encriptar = CreateHash(usuario.Pass);
                    usuario.Pass = encriptar;
                    usuario.Imagen = "UserNew.png";
                    usuario.FecDeCreacion = DateTime.Now;
                    //var agregarUsuario = context.Add(usuario);
                    _context.UsuarioCs.Add(usuario);
                    _context.SaveChanges();
                    return RedirectToAction("InSesion", "Autor");
                }
            }
            catch (Exception)
            {
                return View(usuario);
            }
         
            return View(usuario);
        }
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        /*[HttpGet]
        [Route("EditarUsuario")]
        public IActionResult EditarUsuario(int id)
        {
            try
            {
                var user = HttpContext.Session.Get<Usuario>("sessionUser");

                var user1 = context.Usuarios.Where(o => o.Id == id).First();
                ViewBag.IDSSSS = user1.Id;
                ViewBag.ImagenEditar = user1.Imagen;
                return View(user1);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Auth");
            }
        }


        [Route("EditarUsuario")]
        [HttpPost]
        [Obsolete]
        public IActionResult EditarUsuario(Usuario user, IFormFile photo)
        {
            try
            {
                var userDb = context.Usuarios.Where(o => o.Id == user.Id).First();
                //validar esto

                userDb.Apellido = user.Apellido;
                userDb.Nombre = user.Nombre;
                userDb.Correo = user.Correo;
                userDb.Nickname = user.Nickname;
                userDb.Password = user.Password;

                if (photo.Length > 0)
                {
                    var filePath = Path.Combine(env.WebRootPath, "Images", photo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                }
                user.Imagen = photo.FileName;
                userDb.Imagen = user.Imagen;
                ViewBag.RutaImag = userDb.Imagen;
                string img = userDb.Imagen;
                HttpContext.Session.SetString("Imagen", img);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Auth");
            }

        }*/

        public void validarUsuarios(Usuario usuario)
        {


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
           /* if (!validarLetras(usuario.Nombre))
            {
                ModelState.AddModelError("Nombre1", "Solo ingrese caracteres alfabeticos");
            }

            if (!validarLetras(usuario.Apellido))
            {
                ModelState.AddModelError("Apellido1", "Solo ingrese caracteres alfabeticos");
            }*/
            if (usuario.Correo == null || usuario.Correo == "")
            {
                ModelState.AddModelError("Correo", "El campo Correo es requerido");
            }
            if (usuario.Nick == null || usuario.Nick == "")
            {
                ModelState.AddModelError("Nickname", "El campo Nickname es requerido");
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

        //Metodos de validación
       /* public bool validarLetras(string numString)
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
        }*/
       public ActionResult Interface()
        {
            ViewData["Message"] = LoggedUser().Nombre;
            //var usuarioActivo = new Usuario();
            //var usuarioActivos = LoggedUser().Nombre;
            return PartialView();
        }
        public ActionResult Editar()
        {
           /* ViewBag.Types = _context.Types.ToList();
            ViewBag.Currency = new List<string> { "Euro", "Dolar", "Soles" };

            var cuenta = _context.Cuentas.Where(o => o.Id == id).FirstOrDefault(); // si no lo encuentra retorna un null
            return View("Editar", cuenta);

            // o tambien se hace asi:
            //ViewBag.Cuentas = _context.Cuentas.Where(o => o.Id == id).FirstOrDefault();
            //return View("Editar");*/
            return View();
        }
        public ActionResult CancionesEscuchadas()
        {
            return View();
        }
        public ActionResult ListaDeReproduccion()
        {
            return View();
        }
        public ActionResult DetalleDeCancionActual()
        {
            return PartialView();
        }
        public ActionResult Albumes()
        {
            return View();
        }
    }
}
