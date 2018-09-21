using CAC.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CAC.Interfaces
{    
    [ServiceContract]
    public interface IPriorityPatientService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "listbyfile/{idArchivo}")]
        List<DTOPacientePrioritario> listPriorityPatient(String idArchivo);
           
    }
}
