using CAC.Library.Model.DTO;
using CAC.Library.Model.POCO;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace CAC.Library.BP
{
    public interface IFileProcessBP
    {
        int GetTotalRow();
        List<DTOValidacionArchivo> ValidateFile(string nameFile, byte[] Binario);
        List<TemplateFormatCAC> GetTemplate();
    }
}
