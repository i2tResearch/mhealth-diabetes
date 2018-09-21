using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAC.Library.BP;
using System.IO;

namespace FileUploadUnitTest
{
    [TestClass]
    public class TestPriorityPatient
    {
        [TestMethod]
        public void TestValidatePriorityPatient()
        {
            try
            {
                byte[] binario = File.ReadAllBytes("f:\\prueba.xlsx");
                // llAMAR ESTE METODO
                IPriorityPatient priorityPatient = new PriorityPatient();
                priorityPatient.Build(binario, "f:\\prueba2.xlsx", "fdf55c04-5700-4560-b8b6-9ba26988e7cd");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
                throw;
            }

        }
    }
}
