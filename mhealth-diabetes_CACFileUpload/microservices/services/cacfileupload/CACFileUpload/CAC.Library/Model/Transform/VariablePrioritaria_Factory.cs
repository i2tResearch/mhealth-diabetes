using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAC.Library.Model.Transform
{
    public class VariablePrioritaria_Factory : Transform<tbl_variable_prioritaria, DTOVariablePrioritaria>
    {
        public override DTOVariablePrioritaria transformDTO(tbl_variable_prioritaria modelo)
        {
            DTOVariablePrioritaria response = new DTOVariablePrioritaria();
            if (modelo != null)
            {
                response.Id = modelo.Id.ToString();
                response.NombreVariable = modelo.nombreVariable != null ? modelo.nombreVariable : null;
                response.ValorVariable = modelo.valorVariable != null ? modelo.valorVariable : null;
                response.ValorUmbral = modelo.valorUmbral != null ? modelo.valorUmbral : null;
            }
            return response;
        }

        public override tbl_variable_prioritaria transformModel(DTOVariablePrioritaria dto)
        {
            tbl_variable_prioritaria response = new tbl_variable_prioritaria();
            if (dto != null)
            {
                Guid id = Guid.Empty;
                Guid.TryParse(dto.Id, out id);
                response.Id = id;
                response.nombreVariable = dto.NombreVariable != null ? dto.NombreVariable : null;
                response.valorVariable = dto.ValorVariable != null ? dto.ValorVariable : null;
                response.valorUmbral = dto.ValorUmbral != null ? dto.ValorUmbral : null;
            }
            return response;
        }
    }
}
