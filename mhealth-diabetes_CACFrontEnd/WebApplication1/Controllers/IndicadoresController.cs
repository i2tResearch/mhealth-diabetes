using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAC.Client.Models;
using CACWeb.Properties;
using CAC.Library.Model.DTO;
using System.Globalization;
using CACWeb.Models;

namespace CACWeb.Controllers
{
    public class IndicadoresController : Controller
    {
        //string url = $@"{Resources.URL_TESTING_INDICADORES}/{Resources.API_VERSION_INDICADORES}";
        //string url_totales = $@"{Resources.URL_TESTING_INDICADORES}/{Resources.API_VERSION_INDICADORES_TOTALES}";
        string url = $@"{Resources.URL_LOCAL_INDICADORES}/{Resources.API_VERSION_INDICADORES}";
        string url_totales = $@"{Resources.URL_LOCAL_INDICADORES}/{Resources.API_VERSION_INDICADORES_TOTALES}";
        // GET: Indicadores
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("GetReport")]
        public ActionResult GetReport()
        {
            try
            {
                var currentUser = Session["currentUser"];

                if (currentUser != null)
                {
                    DTOUsuario user = (DTOUsuario)currentUser;
                    if (user.Organizacion != null && user.Organizacion.Id != "")
                    {
                        var temp_fecha_inicio = Request["fecha_inicio"];
                        var temp_fecha_fin = Request["fecha_fin"];
                        var company = Request["company"];
                        CultureInfo provider = CultureInfo.InvariantCulture;
                        DateTime fecha_inicio = DateTime.Now;
                        DateTime fecha_fin = DateTime.Now;

                        var idOrganizacion = company != null ? company : user.Organizacion.Id;
                        var reporte = Request["nombreReporte"];
                        ISynchronizationManager syn = new SynchronizationManager();
                        Request request = new Request();
                        request.Method = "GET";
                        string url_temporal = "";
                        url_temporal = GetReporteURL(reporte, url_temporal);

                        string[] words = temp_fecha_inicio.Split('/');
                        string init = words[2] + "-" + words[0] + "-" + words[1];

                        string[] words2 = temp_fecha_fin.Split('/');
                        string finit = words2[2] + "-" + words2[0] + "-" + words2[1];

                        //CAC.Library.Utilities.IOUtilities.WriteLog($"sys:\t{DateTime.Now}\tincome:\t{temp_fecha_inicio}\toutput:\t{init}\tincome_ent:\t{temp_fecha_fin}\toutput_end:\t{finit}", "Audit", "dates_audit.txt");

                        //request.Url = $@"{url_temporal}?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}";
                        request.Url = $@"{url_temporal}?fechaIni={init}&fechaFin={finit}&idOrganizacion={idOrganizacion}";
                        request.ContentType = "application/json";
                        Response response = syn.GetRequest(request);
                        if (response.TextResponse.Contains("Mes") && !response.TextResponse.Contains("Exception"))
                        {
                            return Json(new JsonResponse() { status = "SERVER_OK", content = response.TextResponse }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new JsonResponse() { status = "NOT_OK", content = response.TextResponse }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                        return Json(new JsonResponse() { status = "NOT_OK", content = "El usuario actual no posee una organización válida para generar el reporte." }, JsonRequestBehavior.AllowGet);
                }
                else
                    return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse() { status = "NOT_OK", content = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        private string GetReporteURL(string reporte, string url_temporal)
        {
            switch (reporte)
            {
                case "getControlDiabetesMellitus":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getControlDiabetesMellitus}";
                    break;
                case "getControlHipertensionArterial":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getControlHipertensionArterial}";
                    break;
                case "getControlLDL":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getControlLDL}";
                    break;
                case "getControlPTH3":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getControlPTH3}";
                    break;
                case "getControlPTH4":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getControlPTH4}";
                    break;
                case "getControlPTH5":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getControlPTH5}";
                    break;
                case "getMedicionAlbuminuria":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getMedicionAlbuminuria}";
                    break;
                case "getMedicionCreatina":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getMedicionCreatina}";
                    break;
                case "getMedicionHbA1c":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getMedicionHbA1c}";
                    break;
                case "getMedicionLDL":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getMedicionLDL}";
                    break;
                case "getMedicionPTH3":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getMedicionPTH3}";
                    break;
                case "getProgresionRenal":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getProgresionRenal}";
                    break;
                case "getMedicionPTH4":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getrMedicionPTH4}";
                    break;
                case "getMedicionPTH5":
                    url_temporal = $@"{url}/{Resources.INDICADORES_getrMedicionPTH5}";
                    break;
                    // ---------- REPORTES TORTA
                case "getHipertensionArterial":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getHipertensionArterial}";
                    break;
                case "getHba1c":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getHba1c}";
                    break;
                case "getLDL":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getLDL}";
                    break;
                case "getAlbuminuria":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getAlbuminuria}";
                    break;
                case "getCreatinina":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getCreatinina}";
                    break;
                case "getProgEnfRenal":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getProgEnfRenal}";
                    break;
                case "getPTH3":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getPTH3}";
                    break;
                case "getPTH4":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getPTH4}";
                    break;
                case "getPTH5":
                    url_temporal = $@"{url_totales}/{Resources.INDICADORES_getPTH5}";
                    break;
            }

            return url_temporal;
        }
    }
}