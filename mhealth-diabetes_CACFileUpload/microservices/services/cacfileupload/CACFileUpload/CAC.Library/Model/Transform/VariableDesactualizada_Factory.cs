using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;

namespace CAC.Library.Model.Transform
{
    public class VariableDesactualizada_Factory : Transform<tbl_variable_desactualizada, DTOVariableDesactualizada>
    {

        public override DTOVariableDesactualizada transformDTO(tbl_variable_desactualizada modelo)
        {
            DTOVariableDesactualizada response = new DTOVariableDesactualizada();
            if (modelo != null)
            {
                response.Id = modelo.Id.ToString();
                response.NombreVariable = modelo.nombreVariable != null ? modelo.nombreVariable : null;
                response.ValorVariable = modelo.valorVariable != null ? modelo.valorVariable : null;
                response.MesesDesactualizados = modelo.mesesDesactualizado != null ? modelo.mesesDesactualizado : null;
            }
            return response;
        }

        public override tbl_variable_desactualizada transformModel(DTOVariableDesactualizada dto)
        {
            tbl_variable_desactualizada response = new tbl_variable_desactualizada();
            if (dto != null)
            {
                Guid id = Guid.Empty;
                Guid.TryParse(dto.Id, out id);
                response.Id = id;
                response.nombreVariable = dto.NombreVariable != null ? dto.NombreVariable : null;
                response.valorVariable = dto.ValorVariable != null ? dto.ValorVariable : null;
                response.mesesDesactualizado = dto.MesesDesactualizados != null ? dto.MesesDesactualizados : null;
            }
            return response;
        }

    }
}
