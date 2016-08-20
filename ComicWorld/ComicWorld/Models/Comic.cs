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
        [Required(ErrorMessage = "Por favor ingrese un titulo")]
        public string titulo { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una descripcion")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una foto")]
        public string foto { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un precio")]
        public int precio { get; set; }
    }
    
}