using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;

namespace CAC.Library.Model.Transform
{
    public class Organizacion_Factory : Transform<tbl_organizacion, DTOOrganizacion>
    {
        public override DTOOrganizacion transformDTO(tbl_organizacion modelo)
        {
            DTOOrganizacion response = new DTOOrganizacion();
            if (modelo != null)
            {
                response.Id = modelo.id.ToString();
                response.NIT = modelo.NIT;
                response.Nombre = modelo.nombre;
                response.Direccion = modelo.direccion;
                response.EPS = modelo.eps;
            }
            return response;
        }

        public override tbl_organizacion transformModel(DTOOrganizacion dto)
        {
            tbl_organizacion response = new tbl_organizacion();
            if (dto != null)
            {
                response.id = Guid.Parse(dto.Id);
                response.NIT = dto.NIT;
                response.nombre = dto.Nombre;
                response.direccion = dto.Direccion;
                response.eps = dto.EPS;
            }
            return response;
        }
    }
}
