using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComicWorld.Models
{
    public class Comic
    {
        [Required(ErrorMessage = "Por favor ingrese un id")]
        public int id { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un título")]
        public string titulo { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una descripción")]
        public string descripcion { get; set; }
        public string foto { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un precio")]
        public int precio { get; set; }
    }
    
}