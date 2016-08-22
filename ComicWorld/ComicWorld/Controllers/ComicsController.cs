using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicWorld.Models;
using System.IO;

namespace ComicWorld.Controllers
{
    [Authorize]
    public class ComicsController : Controller
    {
        Comics lstComics = new Comics();
        Comentarios lstComentarios = new Comentarios();
        //
        // GET: /Comics/
        public ActionResult Index()
        {
            return View(lstComics);
        }
        [HttpPost]
        public ActionResult Index(string busqueda = "")
        {
            lstComics.pmrBusqueda = busqueda;
            return View(lstComics);
        }
        // GET: /Comentarios/
        public ActionResult LstComentarios()
        {
            return View(lstComentarios);
        }
        // GET: /Comentarios/
        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Comic tempComic, HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {
                //Valiar que se cargue una foto
                if (archivo == null)
                {
                    ModelState.AddModelError("", "Por favor ingrese una foto");
                    return View();
                }
                else
                {
                    //Crear nombre de archivo para la foto
                    tempComic.foto = ("/Content/Fotos/"+DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(archivo.FileName));
                    if (!lstComics.AdicionarComic(tempComic))
                    {
                        ModelState.AddModelError("", "Ya existe un comic con el mismo título");
                        return View();
                    }
                    else
                    {
                        //Guardar la foto en el servidor
                        archivo.SaveAs(Server.MapPath(tempComic.foto));
                        //notificar de la acción exitosa
                        TempData["mensaje"] = string.Format("El comic {0} fue creado exitosamente", tempComic.titulo);
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            { 
                return View(); 
            }            
        }
        public ActionResult Detalle(int id = 1)
        {
            Comic temporal = lstComics.ComicporId(id);
            DetalleComic detComic = new DetalleComic { comicConsultado = temporal, lstComentariosCConsultado = lstComentarios.Listado };
            return View(detComic);
        }
	}
}