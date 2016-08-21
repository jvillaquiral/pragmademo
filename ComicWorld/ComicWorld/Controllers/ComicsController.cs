using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicWorld.Models;

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
        // GET: /Comentarios/
        public ActionResult LstComentarios()
        {
            return View(lstComentarios);
        }
	}
}