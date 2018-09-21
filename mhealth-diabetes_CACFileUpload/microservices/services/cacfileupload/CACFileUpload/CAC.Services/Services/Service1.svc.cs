using CAC.Interfaces;
using CAC.Library.Model.DB;
using CAC.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CAC.Library.Model.DTO;
using System.IO;

namespace CAC.Services
{
    public class Service1 : IService1
    {
        public bool Create(DTOUsuario user)
        {
            return true;
        }

        public DTOUsuario GetById(string id)
        {
            return new DTOUsuario() { Apellidos = DateTime.Now.ToString("yyyy-MMMM-dd HH:mm:ss"), Email = "im@fake.com", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumCelular = "123-fake-numb", Organizacion = new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization", NumeroTelefonico = "123-fakes" }, Rol = new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol" }, UIDFirebase = "Fake UIDFirabase" };
        }

        public DTOUsuario GetByEmail(string id)
        {
            return new DTOUsuario() { Apellidos = "Fake", Email = "im@fake.com", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumCelular = "123-fake-numb", Organizacion = new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization", NumeroTelefonico = "123-fakes" }, Rol = new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol" }, UIDFirebase = "Fake UIDFirabase" };
        }

        public DTOArchivo GetFileById(string id)
        {
            return new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), NumFilasImportadas = 534, Tamano = "4687", UrlArchivo = "" };
        }

        public List<DTOArchivo> ListFileByUser(string id)
        {
            List<DTOArchivo> response = new List<DTOArchivo>();
            response.Add(new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), NumFilasImportadas = 534, Tamano = "4687", UrlArchivo = "" });
            response.Add(new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), NumFilasImportadas = 534, Tamano = "4687", UrlArchivo = "" });
            response.Add(new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), NumFilasImportadas = 534, Tamano = "4687", UrlArchivo = "" });
            return response;
        }

        public List<DTOOrganizacion> ListOrganization()
        {
            List<DTOOrganizacion> response = new List<DTOOrganizacion>();
            response.Add(new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization", NumeroTelefonico = "123-fakes" });
            response.Add(new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization too", NumeroTelefonico = "123-fakes" });
            response.Add(new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization me too bae", NumeroTelefonico = "123-fakes" });
            return response;
        }

        public List<DTOPacientePrioritario> ListPatientByPriority(string idArchivo)
        {
            List<DTOPacientePrioritario> response = new List<DTOPacientePrioritario>();
            var temp = new DTOPacientePrioritario() { Apellidos = "Fake", Cedula = "46546513", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumContacto = "123-fake-numb", IdArchivo = Guid.NewGuid().ToString(), ListaVariablesPrioritarias = new List<DTOVariablePrioritaria>() };
            temp.ListaVariablesPrioritarias.Add(new DTOVariablePrioritaria() { Id = Guid.NewGuid().ToString(), NombreVariable = "COSITOMALO", ValorUmbral = "109", ValorVariable = "50" });
            temp.ListaVariablesPrioritarias.Add(new DTOVariablePrioritaria() { Id = Guid.NewGuid().ToString(), NombreVariable = "COSITOMALO", ValorUmbral = "109", ValorVariable = "50" });
            temp.ListaVariablesPrioritarias.Add(new DTOVariablePrioritaria() { Id = Guid.NewGuid().ToString(), NombreVariable = "COSITOMALO", ValorUmbral = "109", ValorVariable = "50" });
            response.Add(temp);

            temp = new DTOPacientePrioritario() { Apellidos = "Fake", Cedula = "46546513", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumContacto = "123-fake-numb", IdArchivo = Guid.NewGuid().ToString(), ListaVariablesPrioritarias = new List<DTOVariablePrioritaria>() };
            temp.ListaVariablesPrioritarias.Add(new DTOVariablePrioritaria() { Id = Guid.NewGuid().ToString(), NombreVariable = "COSITOMALO", ValorUmbral = "109", ValorVariable = "50" });
            temp.ListaVariablesPrioritarias.Add(new DTOVariablePrioritaria() { Id = Guid.NewGuid().ToString(), NombreVariable = "COSITOMALO", ValorUmbral = "109", ValorVariable = "50" });
            temp.ListaVariablesPrioritarias.Add(new DTOVariablePrioritaria() { Id = Guid.NewGuid().ToString(), NombreVariable = "COSITOMALO", ValorUmbral = "109", ValorVariable = "50" });


            return response;
        }

        public List<DTORol> ListRole()
        {
            List<DTORol> response = new List<DTORol>();
            response.Add(new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol" });
            response.Add(new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol too" });
            response.Add(new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake you're so fake in a faking way fake" });
            return response;
        }

        public List<DTOUsuario> ListUser()
        {
            List<DTOUsuario> response = new List<DTOUsuario>();
            response.Add(new DTOUsuario() { Apellidos="Fake", Email = "im@fake.com", Id = Guid.NewGuid().ToString(), Nombres ="Jhon", NumCelular = "123-fake-numb", Organizacion = new DTOOrganizacion () {Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS =true, NIT ="12346579", Nombre ="I'm a fake organization", NumeroTelefonico ="123-fakes" }, Rol = new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion =" I'm a fake rol"}, UIDFirebase = "Fake UIDFirabase" });
            response.Add(new DTOUsuario() { Apellidos = "Fake", Email = "im@fake.com", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumCelular = "123-fake-numb", Organizacion = new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization", NumeroTelefonico = "123-fakes" }, Rol = new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol" }, UIDFirebase = "Fake UIDFirabase" });
            response.Add(new DTOUsuario() { Apellidos = "Fake", Email = "im@fake.com", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumCelular = "123-fake-numb", Organizacion = new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization", NumeroTelefonico = "123-fakes" }, Rol = new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol" }, UIDFirebase = "Fake UIDFirabase" });
            response.Add(new DTOUsuario() { Apellidos = "Fake", Email = "im@fake.com", Id = Guid.NewGuid().ToString(), Nombres = "Jhon", NumCelular = "123-fake-numb", Organizacion = new DTOOrganizacion() { Id = Guid.NewGuid().ToString(), Direccion = "City of fakes, 123-street of fakes", EPS = true, NIT = "12346579", Nombre = "I'm a fake organization", NumeroTelefonico = "123-fakes" }, Rol = new DTORol() { Id = Guid.NewGuid().ToString(), Nombre = "Risk ERC", Descripcion = " I'm a fake rol" }, UIDFirebase = "Fake UIDFirabase" });

            return response;
        }

        public bool ValidatePriorityPatient(Stream log)
        {
            return true;
        }

        public DTOResponse Validator(DTOTransporteArchivo log)
        {
            DTOResponse response = new DTOResponse();
            response.Archivo = new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), NumFilasImportadas = 534, Tamano = "4687", UrlArchivo = "" };
            response.List = new List<DTOValidacionArchivo>() { new DTOValidacionArchivo() { Celda = "A 1", Descripcion = "Valor falso de prueba", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), Valor = "Valor falso que no se envió con tíldes y comilla \"dobles\" y 'simples'" } };
            return response;
        }

        public DTOResponse Validator2(Stream log)
        {
            DTOResponse response = new DTOResponse();
            response.Archivo = new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), NumFilasImportadas = 534, Tamano = "4687", UrlArchivo = "" };
            response.List = new List<DTOValidacionArchivo>();
            response.List.Add(new DTOValidacionArchivo() { Celda = "A 1", Descripcion = "Valor falso de prueba", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), Valor = "Valor falso que no se envió con tíldes y comilla \"dobles\" y 'simples'" });
            response.List.Add(new DTOValidacionArchivo() { Celda = "A 1", Descripcion = "Valor falso de prueba", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), Valor = "Valor falso que no se envió con tíldes y comilla \"dobles\" y 'simples'" });
            response.List.Add(new DTOValidacionArchivo() { Celda = "A 1", Descripcion = "Valor falso de prueba", FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)), Valor = "Valor falso que no se envió con tíldes y comilla \"dobles\" y 'simples'" });
            return response;
        }

        public List<string> Constantes()
        {
            List<string> constantes = new List<string>();
            constantes.Add(Configuration.GetValueConf(Constants.BackupFolder));
            constantes.Add(Configuration.GetValueConf(Constants.CAC_TEMPLATE));
            constantes.Add(Configuration.GetValueConf(Constants.COLUMNS_NAME));
            constantes.Add(Configuration.GetValueConf(Constants.DateFormat));
            constantes.Add(Configuration.GetValueConf(Constants.DATE_TIME_TYPE));
            constantes.Add(Configuration.GetValueConf(Constants.DebuggerFolder));
            constantes.Add(Configuration.GetValueConf(Constants.ExceptionFile));
            constantes.Add(Configuration.GetValueConf(Constants.FullDateTimeFormat));
            constantes.Add(Configuration.GetValueConf(Constants.IsRemote));
            constantes.Add(Configuration.GetValueConf(Constants.NUMBER_TYPE));
            constantes.Add(Configuration.GetValueConf(Constants.NUM_COLUMNS_KEY));
            constantes.Add(Configuration.GetValueConf(Constants.PRIORITY_PATIENT_TEMPLATE));
            constantes.Add(Configuration.GetValueConf(Constants.RootFolder));
            constantes.Add(Configuration.GetValueConf(Constants.TemporalFolder));
            constantes.Add(Configuration.GetValueConf(Constants.TemporalServerFolder));
            constantes.Add(Configuration.GetValueConf(Constants.TimeFormat));
            return constantes;


        }
    }
}
