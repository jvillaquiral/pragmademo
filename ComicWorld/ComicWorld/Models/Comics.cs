using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ComicWorld.Models
{
    public class Comics
    {
        private List<Comic> lstComics = new List<Comic>();
        string keyComics = "lstComics";

        public Comics()
        {
            //Validar que el repositorio de comics ya esté cargado como una variable de aplicación
            if (HttpContext.Current.Application[keyComics] == null)
            {
                //Consumir los comics del servicio JPH
                ConsumirJPH();
                //Crear el repositorio como una variable de aplicación
                HttpContext.Current.Application[keyComics] = lstComics;
            }
            else
            {
                //Obtener el repositorio de la variable de aplicación
                lstComics = (List<Comic>)HttpContext.Current.Application[keyComics];
            }
        }
        public IEnumerable<Comic> Listado
        {
            get { return lstComics; }
        }
        //Método para consumir el servicio JSON placeholder y llenar el repositorio de comics
        public void ConsumirJPH()
        {
            //Hacer el request con get al servicio JSON placeholder
            HttpWebRequest requestServicioJPH = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/posts");
            requestServicioJPH.Method = "GET";

            //Recibir la respuesta del servicio
            WebResponse repuestaJPH = requestServicioJPH.GetResponse();
            Stream campoStream = repuestaJPH.GetResponseStream();
            StreamReader lectorStream = new StreamReader(campoStream);
            string strRespuesta = lectorStream.ReadToEnd();

            //Convertir la respuesta a un repositorio de posts
            List<PostJPH> lista = JsonConvert.DeserializeObject<List<PostJPH>>(strRespuesta.ToString());

            //Cerrar componentes
            campoStream.Close();
            repuestaJPH.Close();

            //Llenar el repositorio de comic con el listado de posts obtenido
            foreach (PostJPH linea in lista)
            {
                lstComics.Add(new Comic { id = linea.id, titulo = linea.title, descripcion = linea.body, foto = "imagen"+linea.id.ToString(), precio = 10000});
            }
        }
    }
    //Declarar clases para los usuarios recibidos de Jsonplaceholder
    public class PostJPH 
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
 
    }
}