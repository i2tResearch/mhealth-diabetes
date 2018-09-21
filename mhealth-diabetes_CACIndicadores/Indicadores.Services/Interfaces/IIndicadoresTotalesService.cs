using Indicadores.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Indicadores.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIndicadoresTotalesService" in both code and config file together.
    [ServiceContract]
    public interface IIndicadoresTotalesService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "getAlbuminuria?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerAlbuminuria(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "getCreatinina?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerCreatinina(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "getHba1c?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerHbA1c(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "getHipertensionArterial?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerHipertensionArterial(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getLDL?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerLDL(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getProgEnfRenal?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerProgEnfRenal(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getPTH3?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerPTH3(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getPTH4?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerPTH4(String fechaIni, String fechaFin, String idOrganizacion);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "getPTH5?fechaIni={fechaIni}&fechaFin={fechaFin}&idOrganizacion={idOrganizacion}")]
        List<DTOIndicadorTotales> ObtenerPTH5(String fechaIni, String fechaFin, String idOrganizacion);

    }
}
