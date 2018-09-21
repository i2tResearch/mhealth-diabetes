using CAC.Interfaces;
using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using CAC.Library.Model.Transform;
using CAC.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAC.Services
{
    /// <summary>
    /// API que gestiona los usuarios
    /// </summary>
    public class User : IUser
    {

        //Una estructura de singleton para asgurar que sólo exista una instación EntityFramework en toda la ejecución en el servidor.
        private SingletonDB controllerDB = SingletonDB.getInstance();
        //representa la conexión a la base datos inicializando un objeto contexto de Entity Framework
        private ModelContainer db = null;

        public User()
        {
            db = controllerDB.DB;
        }
        public bool Create(DTOUsuario user)
        {
            try
            {
                Usuario_Factory usuFactory = new Usuario_Factory();
                tbl_usuario usuario = usuFactory.transformModel(user);

                if (usuario != null)
                {
                    var u = db.tbl_usuario.Add(usuario);
                    int total = db.SaveChanges();
                    IOUtilities.WriteLog(string.Format("{0}\t{1}\tCreate\t{2}", IOUtilities.GetLocalTime(), Configuration.GetClassName<User>(), total), Configuration.GetClassName<User>(), Configuration.GetValueConf(Constants.LogFile));
                }
                return true;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<User>());
                throw;
            }
        }

        public DTOUsuario GetById(string id)
        {
            DTOUsuario response = null;
            try
            {
                Usuario_Factory usuFactory = new Usuario_Factory();
                var temp = db.tbl_usuario.Include("tbl_organizacion").Include("tbl_usuario_rol.tbl_rol").Where(m => m.uid_firebase == id).FirstOrDefault();
                if (temp != null)
                {
                    response = usuFactory.transformDTO(temp);
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<User>());
            }
            return response;
        }

        public List<DTOUsuario> ListUser()
        {
            List<DTOUsuario> response = new List<DTOUsuario>();

            try
            {
                Usuario_Factory usuFactory = new Usuario_Factory();
                List<tbl_usuario> lstUsuarios = db.tbl_usuario.Include("tbl_organizacion").Include("tbl_usuario_rol.tbl_rol").ToList();
                response = usuFactory.transformListDTO(lstUsuarios);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<User>());
            }

            return response.OrderBy(m => m.Nombres).ToList();
        }

        public List<DTORol> ListRole()
        {
            List<DTORol> response = new List<DTORol>();

            try
            {
                Rol_Factory roleFactory = new Rol_Factory();
                List<tbl_rol> lstRole = db.tbl_rol.ToList();
                response = roleFactory.transformListDTO(lstRole);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<User>());
            }

            return response;
        }

        public List<DTOOrganizacion> ListOrganization()
        {
            List<DTOOrganizacion> response = new List<DTOOrganizacion>();

            try
            {
                Organizacion_Factory organizationFactory = new Organizacion_Factory();
                List<tbl_organizacion> lstOrganization = db.tbl_organizacion.ToList();
                response = organizationFactory.transformListDTO(lstOrganization);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<User>());
            }

            return response;
        }

        public DTOUsuario GetByEmail(string id)
        {
            DTOUsuario response = null;
            try
            {
                Usuario_Factory usuFactory = new Usuario_Factory();
                var temp = db.tbl_usuario.Include("tbl_organizacion").Include("tbl_usuario_rol.tbl_rol").Where(m => m.email == id).FirstOrDefault();
                if (temp != null)
                {
                    response = usuFactory.transformDTO(temp);
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<User>());
            }
            return response;
        }
    }
}
