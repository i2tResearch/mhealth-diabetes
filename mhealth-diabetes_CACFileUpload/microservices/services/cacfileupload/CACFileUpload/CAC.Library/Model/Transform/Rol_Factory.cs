using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;

namespace CAC.Library.Model.Transform
{
    public class Rol_Factory : Transform<tbl_rol, DTORol>
    {
        public override DTORol transformDTO(tbl_rol modelo)
        {
            DTORol response = new DTORol();
            if (modelo != null)
            {
                response.Id = modelo.id.ToString();
                response.Nombre = modelo.nombre;
                response.Descripcion = modelo.descripcion;
            }
            return response;
        }

        public override tbl_rol transformModel(DTORol dto)
        {
            tbl_rol response = new tbl_rol();
            if (dto != null)
            {
                response.id = Guid.Parse(dto.Id);
                response.nombre = dto.Nombre;
                response.descripcion = dto.Descripcion;
            }
            return response;
        }
    }
}
