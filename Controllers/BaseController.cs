using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.DataBase;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancistoCloneWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly BeamyContext context;
        public BaseController(BeamyContext context)
        {
            this.context = context;
        }

        protected Usuario LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = context.Usuarios.Where(o => o.Nick == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
