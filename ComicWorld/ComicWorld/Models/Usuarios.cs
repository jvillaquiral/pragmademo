using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ComicWorld.Models
{
    public class Usuarios
    {
        private List<Usuario> lstUsuarios = new List<Usuario>();
        string keyUsuarios = "lstUsuarios";
        public Usuarios()
        {
            //Validar que el repositorio de usuario ya esté cargado como una variable de aplicación
            if (HttpContext.Current.Application[keyUsuarios] == null)
            {
                //Consumir los usuarios del servicio JPH
                ConsumirJPH();
                //Crear el usuario por defecto, para permitir el logueo por primera vez
                Usuario demo = new Usuario { id = 11, usuario = "admin", clave = "admin", nombre = "Usuario Administrador", cedula = "10222333", correo = "javico33@hotmail.com" };
                lstUsuarios.Add(demo);
                //Crear el repositorio como una variable de aplicación
                HttpContext.Current.Application[keyUsuarios] = lstUsuarios;
            }
            else
            {
                //Obtener el repositorio de la variable de aplicación
                lstUsuarios = (List<Usuario>)HttpContext.Current.Application[keyUsuarios];
            }
        }
        public IEnumerable<Usuario> Listado
        {
            get { return lstUsuarios; }
        }

        //Método para consumir el servicio JSON placeholder y llenar el repositorio de usuarios
        public void ConsumirJPH()
        {
            //Hacer el request con get al servicio JSON placeholder
            HttpWebRequest requestServicioJPH = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/users");
            requestServicioJPH.Method = "GET";

            //Recibir la respuesta del servicio
            WebResponse repuestaJPH = requestServicioJPH.GetResponse();
            Stream campoStream = repuestaJPH.GetResponseStream();
            StreamReader lectorStream = new StreamReader(campoStream);
            string strRespuesta = lectorStream.ReadToEnd();

            //Convertir la respuesta al repositorio de usuarios
            List<UserJPH> lista = JsonConvert.DeserializeObject<List<UserJPH>>(strRespuesta.ToString());

            //Cerrar componentes
            campoStream.Close();
            repuestaJPH.Close();

            //Llenar el repositorio de usuarios con el listado obtenido
            foreach (UserJPH linea in lista)
            {
                lstUsuarios.Add(new Usuario { id = linea.id, usuario = linea.username, clave = "pass" + linea.username, nombre = linea.name, correo = linea.email, cedula = linea.id.ToString() });
            }
        }
        //Método para validar usuario
        //Retorna falso si la clave o el usuario no existe
        public bool ValidarUsuario(LoginUsuario usuDato)
        {
            int numResultados = lstUsuarios.Where(x => x.usuario == usuDato.usuario).Where(x => x.clave == usuDato.clave).Count();
            if (numResultados == 0)
                return false;
            else
                return true;
        }
        //Método para adicionar un usuario
        public bool AdicionarUsuario(Usuario usuDato)
        {
            //Validar que el usuario no exista ya
            int usuExistente = lstUsuarios.Where(x => x.usuario == usuDato.usuario).Count();
            if (usuExistente > 0)
                return false;
            else
            {
                //Obtener el mayor id
                int mayorId = lstUsuarios.Max(x => x.id);
                usuDato.id = ++mayorId;
                //Adicionar el usuario al repositorio
                lstUsuarios.Add(usuDato);
                //Recrear la variable de aplicación con la lista modificada
                HttpContext.Current.Application[keyUsuarios] = lstUsuarios;
                return true;
            }
        }
    }
    //Modelo utilizado para recibir los datos de la ventana de login
    public class LoginUsuario
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        public string clave { get; set; }
    }

    //Declarar clases para los usuarios recibidos de Jsonplaceholder
    public class UserJPH
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public AddressJPH address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public CompanyJPH company { get; set; }
    }
    public class AddressJPH
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public GeoJPH geo { get; set; }

    }
    public class GeoJPH
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
    public class CompanyJPH
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }
}