using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ComicWorld.Models
{
    public class Comentarios
    {
        private List<Comentario> lstComentarios = new List<Comentario>();
        string keyComentarios = "lstComentarios";

        public Comentarios()
        {
            //Validar que el repositorio de comentarios ya esté cargado como una variable de aplicación
            if (HttpContext.Current.Application[keyComentarios] == null)
            {
                //Consumir los comentarios del servicio JPH
                ConsumirJPH();
                //Crear el repositorio como una variable de aplicación
                HttpContext.Current.Application[keyComentarios] = lstComentarios;
            }
            else
            {
                //Obtener el repositorio de la variable de aplicación
                lstComentarios = (List<Comentario>)HttpContext.Current.Application[keyComentarios];
            }
        }
        public List<Comentario> Listado
        {
            get { return lstComentarios; }
        }

        //Método para consumir el servicio JSON placeholder y llenar el repositorio de comentarios
        public void ConsumirJPH()
        {
            //Hacer el request con get al servicio JSON placeholder
            HttpWebRequest requestServicioJPH = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/comments");
            requestServicioJPH.Method = "GET";

            //Recibir la respuesta del servicio
            WebResponse repuestaJPH = requestServicioJPH.GetResponse();
            Stream campoStream = repuestaJPH.GetResponseStream();
            StreamReader lectorStream = new StreamReader(campoStream);
            string strRespuesta = lectorStream.ReadToEnd();

            //Convertir la respuesta a un repositorio de comentarios
            List<ComentariosJPH> lista = JsonConvert.DeserializeObject<List<ComentariosJPH>>(strRespuesta.ToString());

            //Cerrar componentes
            campoStream.Close();
            repuestaJPH.Close();

            //Llenar el repositorio de comentarios con el listado obtenido
            foreach (ComentariosJPH linea in lista)
            {
                lstComentarios.Add(new Comentario { id = linea.id, comicId = linea.postId, nombre = linea.name, correo = linea.email, texto = linea.body });
            }
        }
    }
    //Declarar clases para los comentarios recibidos de Jsonplaceholder
    public class ComentariosJPH
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}