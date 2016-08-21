using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicWorld.Models;
using System.Web.Security;


namespace ComicWorld.Controllers
{
    public class HomeController : Controller
    {
        Usuarios lstUsuarios = new Usuarios();
        
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginUsuario logUsuario)
        {
            if (ModelState.IsValid)
            {
                if (lstUsuarios.ValidarUsuario(logUsuario))
                {
                    FormsAuthentication.SetAuthCookie(logUsuario.usuario, false);
                    return RedirectToAction("Index","Comics");
                }
                else
                {
                    ModelState.AddModelError("", "claveo o usuario incorrectos");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Salida()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
	}
}