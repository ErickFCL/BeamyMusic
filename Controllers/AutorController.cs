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

        //[Authorize]
        //public string LoggedUserView()
        //{
        //    return "El usuario Logueado es:" + LoggedUser().Nombre;
        //}
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

        //

        [HttpPost]
        public IActionResult InSesion(string username, string password)
        {
            
             
            var userv = _context.UsuarioCs
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
                return RedirectToAction("Interface", "Usuario");
            }
            ModelState.AddModelError("InSesion", "Nik o contraseña incorectos");
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
       
    }
}
