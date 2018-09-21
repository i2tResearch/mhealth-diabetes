using Indicadores.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Indicadores.Interfaces
{

    [ServiceContract]
    public interface IIndicadoresService
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "getControlHipertensionArterial?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerControlHipertensionArterial(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionHbA1c?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionHbA1c(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getControlDiabetesMellitus?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerControlDiabetesMellitus(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionLDL?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionLDL(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getControlLDL?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerControlLDL(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionAlbuminuria?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionAlbuminuria(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionPTH5?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionPTH5(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getControlPTH5?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerControlPTH5(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionPTH4?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionPTH4(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getControlPTH4?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerControlPTH4(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionPTH3?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionPTH3(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getControlPTH3?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerControlPTH3(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getProgresionRenal?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerProgresionRenal(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getMedicionCreatina?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicador> ObtenerMedicionCreatina(String fechaIni, String fechaFin, String idOrganizacion);

    }
}
