using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicWorld.Models;

namespace ComicWorld.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        Usuarios lstUsuarios = new Usuarios();
        //
        // GET: /Usuarios/
        public ActionResult Index()
        {
            return View(lstUsuarios);
        }
        // GET: /Usuario/
        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]    
        public ActionResult Crear(Usuario tempUsuario)
        {
            if (ModelState.IsValid)
            {
                if (!lstUsuarios.AdicionarUsuario(tempUsuario))
                {
                    ModelState.AddModelError("", "El usuario ya existe");
                    return View();
                }
                else
                {
                    //notificar de la acción exitosa
                    TempData["mensaje"] = string.Format("El usuario {0} fue creado exitosamente", tempUsuario.usuario);
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                return View();
            }
        }
	}
}