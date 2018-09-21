using CAC.Client.Models;
using CAC.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CACWeb.Properties;
using CAC.Library.Utilities;

namespace CACWeb.Controllers
{
    public class DataController : Controller
    {

        #region CAC Uploadfile
        //URLS DE USUARIOS

        string url = $@"{Resources.URL_TESTING_FILEUPLOAD}/{Resources.API_VERSION_FILEUPLOAD}";
        
        // GET: Data
        public ActionResult Index()
        {
            return Content("Chugar");
        }
        
        //ADD -- LISTO
        [HttpPost]
        public ActionResult getUsuarioPorCorreo()
        {
            string correo = Request["correo"];
            var response = "";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.USER_GETBYEMAIL_GET}/{correo}");
                myRequest.Method = "GET";
                myRequest.ContentType = "application/json";

                var webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                JavaScriptSerializer json = new JavaScriptSerializer();
                var inicio = json.Deserialize<DTOUsuario>(response);
                Session["currentUser"] = inicio;
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }
        
        //LIST -- LISTO
        [HttpGet]
        public ActionResult getUsuariosList()
        {
            var response = "";
            try
            {

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.USER_LIST_GET}");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        //ADD  -- LISTO
        [HttpPost]
        public ActionResult crearUsuario()
        {
            string json = Request["datos"];
            var response = "";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.USER_ADD_POST}");

                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";

                /*JsonSerializer serializer = new JsonSerializer();

                StringReader sr = new StringReader(json);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

                JsonRequest jsonRequest = (JsonRequest)serializer.Deserialize(reader, typeof(JsonRequest));

                //do work with object*/

                using (var streamWriter = new StreamWriter(myRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        //LISTROLE --LISTO  
        [HttpGet]
        public ActionResult getRolesList()
        {

            var response = "";
            try
            {
                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.USER_LIST_ROLE_GET}");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        //LISTORGANIZATION -- LISTO
        [HttpGet]
        public ActionResult getEmpresasList()
        {
            var response = "";
            try
            {
                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.USER_LIST_ORGANIZATION_GET}");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        //VALIDATOR1 -- LISTO
        [HttpPost]
        [ActionName("UploadFile")]
        public ActionResult UploadFileValidator(HttpPostedFileBase file = null)
        {
            string id_usuario = Request["user_id"];
            if (file != null)
            {
                DTOTransporteArchivo dtotransporte = new DTOTransporteArchivo();

                DTOArchivo archivo = new DTOArchivo();
                archivo.FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                archivo.Id = Guid.NewGuid().ToString();
                archivo.Tamano = file.ContentLength.ToString();
                archivo.Nombre = file.FileName;
                archivo.IdUsuario = id_usuario;

                try
                {
                    // convert xmlstring to byte using ascii encoding
                    byte[] binario;
                    byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        binario = ms.ToArray();
                    }
                    byte[] data = null;
                    string directory = System.Web.Hosting.HostingEnvironment.MapPath($@"{Resources.TemporalServerFolder}");
                    string path_file = $@"{directory}/{file.FileName}";
                    if(Directory.Exists(directory) == false)
                    {
                        Directory.CreateDirectory(directory);
                    }
                    if (System.IO.File.Exists(path_file) == false)
                    {
                        // Create the file.
                        using (FileStream fs = System.IO.File.Create(path_file))
                        {
                           fs.Write(binario, 0, binario.Length);
                        }
                    }else
                    {
                        System.IO.File.WriteAllBytes(path_file, binario);
                    }
                    
                    FileInfo fileinfo = new FileInfo(path_file);
                    string compressfilename = ZipFile.CompressGZipStream(fileinfo);
                    if (compressfilename != null)
                    {
                        FileStream fs = new FileStream(compressfilename, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        data = br.ReadBytes((int)fs.Length);
                        br.Close();
                        fs.Close();
                        System.IO.File.Delete(compressfilename);
                        System.IO.File.Delete(path_file);
                    }
                    dtotransporte.Binario = data;
                    dtotransporte.Archivo = archivo;
                    ISynchronizationManager syn = new SynchronizationManager();
                    Request request = new Request();
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    json_serializer.MaxJsonLength = 2147483647;
                    request.Content = json_serializer.Serialize(dtotransporte);
                    request.ContentType = "application/json";
                    request.Method = "POST";
                    request.Url = $@"{url}/{Resources.FILE_VALIDATOR_POST}";

                    Response response = syn.PostRequest(request);                   
                    return Json(response.TextResponse);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return null;
        }

        //VALIDATOR2 -- NO SE USA
        [HttpPost]
        [ActionName("Validator2")]
        public JsonResult UploadFile(HttpPostedFileBase file = null)
        {
            var f = file;
            var response = "";
            try
            {

                // convert xmlstring to byte using ascii encoding
                byte[] binario;
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    binario = ms.ToArray();
                }
                var data = binario;
                // declare httpwebrequet wrt url defined above
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.FILE_VALIDATOR_TESTING_POST}");
                //HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/file/validator2");
                // set method as post
                webrequest.Method = "POST";
                // set content type
                webrequest.ContentType = "application/x-www-form-urlenconded";
                // set content length
                webrequest.ContentLength = data.Length;
                webrequest.Timeout = 300000;
                // get stream data out of webrequest object
                Stream newStream = webrequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                // declare & read response from service
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return Json( ex.ToString() );
            }
            return Json(response);
        }


        //LISTBYUSER -- LISTO
        [HttpGet]
        public ActionResult getArchivosLista()
        {
            var userId = Request["user_id"];
            var response = "";
            try
            {

                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.FILE_LISTBYUSER_GET}/{userId}");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        //LISTBYUSER -- 
        [HttpGet]
        public ActionResult getListaDePrioritarios()
        {
            var fileId = Request["file_id"];
            var response = "";
            try
            {

                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create($@"{url}/{Resources.PRIORITYPATIENT_LISTBYFILE_GET}/{fileId}");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

#endregion

        //#region Indicadores
        ////R1
        //[HttpGet]
        //public ActionResult controlHipertensionArterial()
        //{
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio+","+fecha_fin;
        //    return Content(response);
        //}

        ////R2
        //[HttpGet]
        //public ActionResult medicionHBA1C() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R3
        //[HttpGet]
        //public ActionResult conrolDeDiabetesMelitus() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////------------------------------------------->


        ////R4
        //[HttpGet]
        //public ActionResult medicionLDL()
        //{
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R5
        //[HttpGet]
        //public ActionResult controlLDL() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////------------------------------------------->

        ////R6
        //[HttpGet]
        //public ActionResult medicionDeAlbuminuria() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R7
        //[HttpGet]
        //public ActionResult progresionTFG() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R8
        //[HttpGet]
        //public ActionResult tiempoDeCreatinina() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////------------------------------------------->
        ////R9
        //[HttpGet]
        //public ActionResult PTH1progeso() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R10
        //[HttpGet]
        //public ActionResult PTH1resultado() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////------------------------------------------->

        ////R11
        //[HttpGet]
        //public ActionResult PTH2progeso() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R12
        //[HttpGet]
        //public ActionResult PTH2resultado() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////------------------------------------------->

        ////R13
        //[HttpGet]
        //public ActionResult PTH3progeso() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}

        ////R14
        //[HttpGet]
        //public ActionResult PTH3resultado() {
        //    //DIA-MES-ANIO
        //    var fecha_inicio = Request["fecha_inicio"];
        //    var fecha_fin = Request["fecha_fin"];
        //    var response = "";
        //    try
        //    {

        //        HttpWebRequest myRequest =
        //            (HttpWebRequest)WebRequest.Create("http://0.0.0.0:0000/api/cac/v1/fake/patientpriority/" + "1");

        //        myRequest.Method = "GET";
        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
        //        using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
        //        {
        //            response = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    response = fecha_inicio + "," + fecha_fin;
        //    return Content(response);
        //}
        //#endregion

    }

}