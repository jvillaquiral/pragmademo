using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicWorld.Models;

namespace ComicWorld.Controllers
{
    public class UsuariosController : Controller
    {
        Usuarios lstUsuarios = new Usuarios();
        //
        // GET: /Usuarios/
        public ActionResult Index()
        {
            return View(lstUsuarios);
        }
	}
}