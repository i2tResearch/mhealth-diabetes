using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAC.Library.Model.Transform
{
    public class Usuario_Factory : Transform<tbl_usuario, DTOUsuario>
    {

        private Rol_Factory rolFactory;
        private Organizacion_Factory organizacionFactory;

        public Usuario_Factory()
        {
            rolFactory = new Rol_Factory();
            organizacionFactory = new Organizacion_Factory();
        }

        public override DTOUsuario transformDTO(tbl_usuario modelo)
        {
            DTOUsuario response = new DTOUsuario();
            if (modelo != null)
            {
                response.Apellidos = modelo.apellidos;
                response.Email = modelo.email;
                response.Id = modelo.id.ToString();
                response.Nombres = modelo.nombres;
                response.NumCelular = modelo.numCelular;
                response.UIDFirebase = modelo.uid_firebase;
                if (modelo.tbl_usuario_rol != null && modelo.tbl_usuario_rol.Count() > 0)
                {
                    tbl_usuario_rol usuario_rol = modelo.tbl_usuario_rol.FirstOrDefault();
                    if (usuario_rol != null && usuario_rol.tbl_rol != null)
                    {
                        response.Rol = rolFactory.transformDTO(usuario_rol.tbl_rol);
                    }
                }
                response.Organizacion = organizacionFactory.transformDTO(modelo.tbl_organizacion);
            }
            return response;
        }

        public override tbl_usuario transformModel(DTOUsuario dto)
        {
            tbl_usuario response = new tbl_usuario();
            if (dto != null)
            {
                response.apellidos = dto.Apellidos;
                response.email = dto.Email;
                response.id = Guid.Parse(dto.Id);
                response.nombres = dto.Nombres;
                response.numCelular = dto.NumCelular;
                response.uid_firebase = dto.UIDFirebase;
                response.tbl_usuario_rol = new List<tbl_usuario_rol>();

                if (dto.Rol != null)
                {
                    tbl_rol rol = rolFactory.transformModel(dto.Rol);

                    if (rol != null)
                    {
                        tbl_usuario_rol usuario_rol = new tbl_usuario_rol();
                        usuario_rol.id = Guid.NewGuid();
                        usuario_rol.idRol = rol.id;
                        //usuario_rol.tbl_rol = rol;
                        usuario_rol.idUsuario = response.id;
                        response.tbl_usuario_rol.Add(usuario_rol);
                    }
                }
                //response.tbl_organizacion = dto.Organizacion != null ? organizacionFactory.transformModel(dto.Organizacion) : null;
                response.idOrganizacion = dto.Organizacion != null ? Guid.Parse(dto.Organizacion.Id) : Guid.Empty;
            }
            return response;
        }
    }
}
