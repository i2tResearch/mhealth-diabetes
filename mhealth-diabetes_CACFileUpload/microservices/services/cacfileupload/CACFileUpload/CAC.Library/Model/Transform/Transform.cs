using System.Collections.Generic;

namespace CAC.Library.Model.Transform
{
    public abstract class Transform<Model, DTO>
    {

        public abstract DTO transformDTO(Model modelo);

        public abstract Model transformModel(DTO dto);

        public List<DTO> transformListDTO(List<Model> lstModel)
        {
            List<DTO> lstDTO = new List<DTO>();

            if (lstModel != null)
            {

                foreach (Model model in lstModel)
                {
                    lstDTO.Add(transformDTO(model));
                }

            }

            return lstDTO;
        }

        public List<Model> transformListModel(List<DTO> lstDTO)
        {

            List<Model> lstModel = new List<Model>();

            if (lstDTO != null)
            {

                foreach (DTO dto in lstDTO)
                {
                    lstModel.Add(transformModel(dto));
                }

            }

            return lstModel;
        }



    }
}
