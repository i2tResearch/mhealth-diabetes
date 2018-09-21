using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;

namespace CAC.Library.Model.Transform
{
    public class Archivo_Factory : Transform<tbl_archivo_cac, DTOArchivo>
    {
        public override DTOArchivo transformDTO(tbl_archivo_cac modelo)
        {
            DTOArchivo response = new DTOArchivo();
            if (modelo != null)
            {
                response.Id = modelo.id.ToString();
                response.FechaCreacion = modelo.fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");
                response.IdUsuario = modelo.tbl_usuario.id.ToString();
                response.Nombre = modelo.nombre;
                response.NumFilasImportadas = modelo.numFilasImportadas;
                response.Tamano = modelo.tamano;
                response.UrlArchivo = modelo.urlArchivo;
            }
            return response;
        }

        public override tbl_archivo_cac transformModel(DTOArchivo dto)
        {
            tbl_archivo_cac response = new tbl_archivo_cac();
            if (dto != null)
            {
                response.id = Guid.Parse(dto.Id);
                response.fechaCreacion = DateTime.Parse(dto.FechaCreacion);
                response.idUsuario = Guid.Parse(dto.IdUsuario);
                response.nombre = dto.Nombre;
                response.numFilasImportadas = dto.NumFilasImportadas;
                response.tamano = dto.Tamano;
                response.urlArchivo = dto.UrlArchivo;
            }
            return response;
        }
    }
}
