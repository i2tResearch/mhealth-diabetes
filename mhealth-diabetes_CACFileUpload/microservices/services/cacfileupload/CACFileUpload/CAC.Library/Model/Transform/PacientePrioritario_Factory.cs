using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;
using System.Collections.Generic;

namespace CAC.Library.Model.Transform
{
    public class PacientePrioritario_Factory : Transform<tbl_paciente_prioritario, DTOPacientePrioritario>
    {
        public override DTOPacientePrioritario transformDTO(tbl_paciente_prioritario modelo)
        {
            DTOPacientePrioritario response = new DTOPacientePrioritario();

            if (modelo != null)
            {
                response.Id = modelo.id.ToString();
                response.Nombres = modelo.nombres;
                response.Apellidos = modelo.apellidos;
                response.Cedula = modelo.cedula;
                response.NumContacto = modelo.numContacto;
                response.IdArchivo = modelo.idArchivo.ToString();

                List<tbl_variable_prioritaria> listModel = new List<tbl_variable_prioritaria>(modelo.tbl_variable_prioritaria);
                response.ListaVariablesPrioritarias = new VariablePrioritaria_Factory().transformListDTO(listModel);

                List<tbl_variable_desactualizada> listModelDesactualizada = new List<tbl_variable_desactualizada>(modelo.tbl_variable_desactualizada);
                response.ListaVariablesDesactualizadas = new VariableDesactualizada_Factory().transformListDTO(listModelDesactualizada);
            }
            return response;
        }

        public override tbl_paciente_prioritario transformModel(DTOPacientePrioritario dto)
        {
            tbl_paciente_prioritario response = new tbl_paciente_prioritario();

            if (dto != null)
            {
                response.id = Guid.Parse(dto.Id);
                response.nombres = dto.Nombres;
                response.apellidos = dto.Apellidos;
                response.cedula = dto.Cedula;
                response.numContacto = dto.NumContacto;
                response.idArchivo = Guid.Parse(dto.IdArchivo);
            }
            return response;
        }
    }
}
