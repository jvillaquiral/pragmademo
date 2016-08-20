using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComicWorld.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "Por favor ingrese un id")]
        public int id { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un usuario")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una clave")]
        public string clave { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una cedula")]
        public string cedula { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un correo")]
        public string correo { get; set; }
    }
}