using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BeamyMusic.DataBase;
using BeamyMusic.Models;
using FinancistoCloneWeb.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BeamyMusic.Controllers
{
  
    public class AutorController : BaseController
    {

        private readonly BeamyContext _context;
        private readonly IConfiguration configuration;

        public AutorController(BeamyContext context, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this.configuration = configuration;
        }
        [HttpGet]
        public string Index(string input)
        {
            return CreateHash(input);
        }
       
        [HttpGet]
        public IActionResult InSesion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InSesion( string username, string password)
        {
          
            validarSesion(username, password);
            if (ModelState.IsValid)
            {   
                var userv = _context.Usuarios
                .Where(o => o.Nick == username && o.Pass == CreateHash(password)).FirstOrDefault();
                if (userv != null)
                {

                    var claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Name, username)
                     };
                    var claimsIdentity = new ClaimsIdentity(claims, "InSesion");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(claimsPrincipal);
                    if (username == "Erick" || username == "erick")
                    {
                        return RedirectToAction("Index","Admin");
                    }
                    else
                    {
                        return RedirectToAction("Interface", "Usuario");
                    }
                   
                }
            }
            else
            {
                ModelState.AddModelError("InSesion", "Nik o contraseña incorectos");
            }
            return View();
        }
       
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("InSesion", "Autor");
        }
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
        public void validarSesion(string username, string password)
        {
             if (username == null || username == "")
                ModelState.AddModelError("Usuario", "El campo usuario es requerido");
            if (password == null || password == "")
                ModelState.AddModelError("Contraseña", "El campo Contraseña es requerido");

        }
        [HttpGet]
        public ActionResult Registrar()
        {

            return View("Registrar", new Usuario());
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
                    _context.Usuarios.Add(usuario);
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
