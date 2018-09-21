using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAC.Library.Model.DTO;
using System.IO;
using CAC.Library.BP;
using System.Collections.Generic;
using CAC.Library.Utilities;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DTOResponse response = new DTOResponse();

            byte[] binario = File.ReadAllBytes(@"D:\Users\Candy\Documents\Debugger SND.CAC\Pruebas_mayo\temp_prueba_tipo_datos.xlsx");
            try
            {
                byte[] buffer = new byte[16 * 1024];
                FileProcessBP filebp = new FileProcessBP();
               

                var template = filebp.GetTemplate();
                
                foreach(var item in template)
                {
                    IOUtilities.WriteLog(item.Name, @"Pruebas_mayo", "template.txt",false);
                }

                List<DTOValidacionArchivo> lista = filebp.ValidateFile("nombre", binario);

                if (lista.Count == 0)
                {
                    response.Archivo = new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NumFilasImportadas = 534, Tamano = binario.Length.ToString(), UrlArchivo = "" };
                    //Run(binario, response.Archivo);

                }
                if (lista.Count > 0)
                {
                    response.List = lista;
                }
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                var stri = js.Serialize(response);
                File.AppendAllText(@"D:\Users\Candy\Documents\Debugger SND.CAC\Pruebas_mayo\response_testing.json", stri);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
