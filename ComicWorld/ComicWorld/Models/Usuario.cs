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
        [RegularExpression(@"^(?=.*[A-Z])(?=(?:.*\d){2})(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "La clave debe contener mín 8 caracteres, 1 mayúscula, 2 número y 1 caracter especial")]
        [Required(ErrorMessage = "Por favor ingrese una clave")]
        public string clave { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un nombre")]
        public string nombre { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "La cédula sólo debe contener números")]
        [Required(ErrorMessage = "Por favor ingrese una cédula")]
        public string cedula { get; set; }
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        [Required(ErrorMessage = "Por favor ingrese un correo")]
        public string correo { get; set; }
    }
}