using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicWorld.Models
{
    public class Comentario
    {
        public int id { get; set; }
        public int comicId { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string texto { get; set; }
    }
}