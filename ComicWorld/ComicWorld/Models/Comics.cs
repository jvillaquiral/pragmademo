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
        private string keyComics = "lstComics";
        public string pmrBusqueda = "";

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
            get {
                if (pmrBusqueda.Length == 0)
                    return lstComics;
                else
                    if (pmrBusqueda.Contains(' '))
                    {
                        string[] cadBusqueda = pmrBusqueda.Split(' ');
                        IEnumerable<Comic> temporal = new List<Comic> ();
                        foreach(string dato in cadBusqueda)
                        {
                            temporal = temporal.Union(lstComics.Where(x => x.titulo.Contains(dato))).ToList();
                        }
                        return temporal;
                    }
                    else
                        return lstComics.Where(x => x.titulo.Contains(pmrBusqueda));
            }
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
            int intContadorFoto = 1;
            foreach (PostJPH linea in lista)
            {
                lstComics.Add(new Comic { id = linea.id, titulo = linea.title, descripcion = linea.body, foto = "/Content/Fotos/comic" + intContadorFoto +".jpg", precio = 10000 });
                intContadorFoto++;
                if (intContadorFoto > 32)
                    intContadorFoto = 1;
            }
        }
        //Método para adicionar un comic
        public bool AdicionarComic(Comic comDato)
        {
            //Validar que el título no exista ya
            int titExistente = lstComics.Where(x => x.titulo == comDato.titulo).Count();
            if (titExistente > 0)
                return false;
            else
            {
                //Obtener el mayor id
                int mayorId = lstComics.Max(x => x.id);
                comDato.id = ++mayorId;
                //Adicionar el usuario al repositorio
                lstComics.Add(comDato);
                //Recrear la variable de aplicación con la lista modificada
                HttpContext.Current.Application[keyComics] = lstComics;
                return true;
            }
        }
        public Comic ComicporId(int prmIdComir)
        {
            return lstComics.Find(x => x.id == prmIdComir);
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
    //Declarar una estructura temporal para el detalle del comic
    public class DetalleComic
    {
        public Comic comicConsultado { get; set; }
        public List<Comentario> lstComentariosCConsultado { get; set; }
    }
}