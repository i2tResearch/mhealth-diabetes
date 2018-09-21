using CAC.Library.Model.DB;
using CAC.Library.Model.POCO;
using CAC.Library.Utilities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CAC.Library.BP
{
    public class PriorityPatient : IPriorityPatient
    {
        private WrapperObject<tbl_variable_desactualizada, string, string> desactualizado = new WrapperObject<tbl_variable_desactualizada, string, string>();
        private WrapperObject<tbl_variable_prioritaria, string, string> variable_prioritaria = new WrapperObject<tbl_variable_prioritaria, string, string>();
        private WrapperObject<string, int, tbl_paciente_prioritario> wrapper = new WrapperObject<string, int, tbl_paciente_prioritario>();
        private string idArchivo;
        
        public void Build(byte[] binaryFile, string idArchivo, string nameFile)
        {
            try
            {
                this.idArchivo = idArchivo;
                desactualizado = new WrapperObject<tbl_variable_desactualizada, string, string>();
                variable_prioritaria = new WrapperObject<tbl_variable_prioritaria, string, string>();
                wrapper = new WrapperObject<string, int, tbl_paciente_prioritario>();
                IXLTableRows rows = initFileWorkSheet(binaryFile, nameFile);
                if(rows != null)
                {
                    ValidatePriorityPatient(rows);
                    SaveData();
                    //Task.Factory.StartNew(() => ValidatePriorityPatient(rows));
                    //Task.Factory.StartNew(() => SaveData());
                    //Task.Factory.StartNew(() => Analsis());
                }
                else
                {
                    throw new Exception("El archivo enviado no tiene un formato valido para se analizado. Debe ser CSV, XLS o XLSX");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        private void Analsis()
        {
            foreach (var temp in desactualizado.GetList())
            {
                string content = $"{temp.tbl_paciente_prioritario.nombres}@{temp.nombreVariable}@{temp.valorVariable}@{temp.mesesDesactualizado}";
                //IOUtilities.WriteLog(content, @"PILOTO", $@"{DateTime.Now.ToString("yyyy-MM-dd HH")}_variable_desactualizadas.txt");
            }

            desactualizado = new WrapperObject<tbl_variable_desactualizada, string, string>();
            foreach (var temp in variable_prioritaria.GetList())
            {
                string content = $"{temp.tbl_paciente_prioritario.nombres}@{temp.nombreVariable}@{temp.valorVariable}@{temp.valorUmbral}";
                //IOUtilities.WriteLog(content, @"PILOTO", $@"{DateTime.Now.ToString("yyyy-MM-dd HH")}_variable_prioritaria.txt");
            }
            variable_prioritaria = new WrapperObject<tbl_variable_prioritaria, string, string>();
        }

        private IXLTableRows initFileWorkSheet(byte[] binaryFile, string nameFile)
        {
            try
            {
                
                if (nameFile.Contains(".csv") == true)
                {
                    IConvertFormat convert = new ConvertFormat();
                    var workBookCAC = convert.Convert_CSV_ClosedXML("page1", binaryFile, ',');
                    var worksheetCAC = workBookCAC.Worksheet(1);
                    var range = worksheetCAC.Range(worksheetCAC.FirstCellUsed(), worksheetCAC.LastCellUsed());
                    range.Clear(XLClearOptions.Formats);
                    var table = range.AsTable();
                    table.DeleteComments();
                    return table.DataRange.Rows();
                }
                else if (nameFile.Contains(".xls") == true || nameFile.Contains(".xlsx"))
                {
                    using (Stream streamFile = new MemoryStream(binaryFile))
                    {
                        using (var workBookCAC = new XLWorkbook(streamFile))
                        {
                            var worksheetCAC = workBookCAC.Worksheet(1);
                            var range = worksheetCAC.Range(worksheetCAC.FirstCellUsed(), worksheetCAC.LastCellUsed());
                            range.Clear(XLClearOptions.Formats);
                            var table = range.AsTable();
                            table.DeleteComments();
                            return table.DataRange.Rows();
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
                throw ex;
            }
        }
        /// <summary>
        /// Recorre las filas del archivo y ejecuta las reglas de cada columna de acuerdo al archivo JSON
        /// </summary>
        private void ValidatePriorityPatient(IXLTableRows rows)
        {
            try
            {
                List<Task> tasks = new List<Task>();
                int k = 0;
                foreach (var row in rows)
                {
                    var v = insertPriorityPatient(row);
                    if (v != null)
                    {
                        var cell = row.Cell(1).Value.ToString();
                        Task t1 = Task.Factory.StartNew(() => RowValidator(row));
                        tasks.Add(t1);
                    }
                    k++;
                }
                Task.WaitAll(tasks.ToArray());
                var exception = tasks.Exists(m => m.Exception != null || m.Status == TaskStatus.Faulted || m.Status == TaskStatus.Canceled);
                if (exception == true)
                {
                    string cause = "";
                    tasks.Where(m => m.Exception != null || m.Status == TaskStatus.Faulted || m.Status == TaskStatus.Canceled).ToList().ForEach(m => cause = $"Task ID:\t{m.Id}\tStatus:\t{m.Status}@{cause}");
                    throw new Exception(cause);
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
            }

        }

        private void RowValidator(IXLTableRow row)
        {
            try
            {
                List<TemplateRulesPriorityPatient> templatePriority = this.GetTemplate();
                foreach (TemplateRulesPriorityPatient template in templatePriority)
                {
                    if (!row.Field(template.Name).GetString().Trim().Equals(""))
                    {
                        ColumnValidator(template, row.Field(template.Name), row);
                    }
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
            }
        }

        private void ColumnValidator(TemplateRulesPriorityPatient template, IXLCell columndata, IXLTableRow row)
        {
            // Si el tipo es DATETIME
            if (template.Type == Configuration.GetValueConf(Constants.DATE_TIME_TYPE))
            {
                // Se valida si la fecha del examen esta desactualizada
                if (template.ValidateOutdated && !columndata.GetString().Trim().Equals(""))
                {
                    if (template.UnknowValue != null
                        && template.NotApply != null
                        && !template.UnknowValue.Equals("")
                        && !template.NotApply.Equals(""))
                    {
                        // Se compara si el valor es el de default si es no se realiza validaciones

                        if (Convert.ToDateTime(columndata.GetString().Trim()).Equals(Convert.ToDateTime(template.UnknowValue))
                            || Convert.ToDateTime(columndata.GetString().Trim()).Equals(Convert.ToDateTime(template.NotApply)))
                        {
                            return;
                        }
                    }

                    try
                    {
                        DateTime dateValidation = DateTime.Now.AddMonths(template.MonthOutdated * -1);

                        if (columndata.GetDateTime() < dateValidation)
                        {
                            var val = columndata.GetDateTime() < DateTime.Now ? DateTime.Now - columndata.GetDateTime() : columndata.GetDateTime() - DateTime.Now;
                            string result = $"Vigencia minima: {template.MonthOutdated} Total: {(int)Math.Truncate((val.TotalDays) / 30)}";
                            // Inserta en variables desactualizadas y no realiza mas validaciones
                            insertOutdated(template.Name, columndata.GetString(), result, row);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
                    }

                }
            }

            // Existe un valor desconocido para la columna tipo fecha
            if ((template.UnknowValue != null && !template.UnknowValue.Equals("")) || (template.NotApply != null && !template.NotApply.Equals("")))
            {
                // Se compara si el valor es el de default si es no se realiza validaciones
                if (Convert.ToDateTime(columndata.GetString().Trim()).Equals(Convert.ToDateTime(template.UnknowValue))
                    || Convert.ToDateTime(columndata.GetString().Trim()).Equals(Convert.ToDateTime(template.NotApply)))
                {
                    return;
                }
            }

            // Existen un valores permitidos para la columna tipo numero
            if (template.AllowValues != null && !template.AllowValues.Equals(""))
            {
                int dataTmp;

                if (int.TryParse(columndata.GetString(), out dataTmp))
                {
                    if (template.AllowValues.Split(',').Where(val => Convert.ToInt32(val) == dataTmp).Count() != 0)
                    {
                        return;
                    }
                }

            }

            validateRangeEq(template, row, columndata.GetString(), template.Type);

            validateRangeDif(template, row, columndata.GetString(), template.Type);

            validateRangeMin(template, row, columndata.GetString());

            validateRangeHigher(template, row, columndata.GetString());

        }
        /// <summary>
        /// Este metodo es el encargardo de validar que los valores de las columnas sean mayor a un valor o
        /// a otra columna dependiento la regla configurada en el archivo JSON.
        /// </summary>
        /// <param name="template">Reglas de la columna</param>
        /// <param name="row">El registro o fila del archivo</param>
        /// <param name="value">EL valor de la columna*fila</param>
        private void validateRangeHigher(TemplateRulesPriorityPatient template, IXLTableRow row, object value)
        {

            RangeHigher[] lstRangeHigher;

            if (template.LstRangeHigher != null && template.LstRangeHigher.Length > 0)
            {
                string valueTmp = value.ToString();

                lstRangeHigher = template.LstRangeHigher;

                double tmpValueInt;
                double tmpValueDb;

                foreach (var rangeHigh in lstRangeHigher)
                {
                    if (rangeHigh.HigEq.Equals("sys"))
                    {

                        if (Convert.ToDateTime(valueTmp) < DateTime.Now)
                        {
                            //Insertar en la tabla de pacientes prioritarios
                            insertVariablePriorityPatient(template.Name, Convert.ToDateTime(valueTmp).ToShortDateString(), $"239@validateRangeHigher@>@Column@{rangeHigh.Column}@value@{rangeHigh.HigEq}", row);
                        }

                        continue;
                    }

                    string[] vals = rangeHigh.HigEq.Split('|');

                    if (vals.Length > 0 && vals[0].Equals("column"))
                    {
                        if (double.TryParse(valueTmp, out tmpValueInt))
                        {
                            if (tmpValueInt < Convert.ToDouble(row.Cell(vals[1]).GetString()))
                            {
                                insertVariablePriorityPatient(template.Name, valueTmp, $"253@validateRangeHigher@>=@Column@{vals[1]}@value@{row.Cell(vals[1]).GetString()}", row);
                            }

                            continue;
                        }

                        if (Convert.ToDateTime(valueTmp) < row.Cell(vals[1]).GetDateTime())
                        {
                            insertVariablePriorityPatient(template.Name, valueTmp, $"261@validateRangeHigher@>=@Column@{vals[1]}@value@{row.Cell(vals[1]).GetString()}", row);
                        }

                        continue;

                    }

                    if (double.TryParse(valueTmp, out tmpValueInt))
                    {

                        double higEqtmp;

                        if (double.TryParse(rangeHigh.HigEq, out higEqtmp))
                        {
                            if (tmpValueInt < higEqtmp)
                            {
                                insertVariablePriorityPatient(template.Name, valueTmp, $"277@validateRangeHigher@>=@Column@{rangeHigh.Column}@value@{rangeHigh.HigEq}", row);
                            }

                            continue;
                        }

                        if (tmpValueInt < Convert.ToDouble(rangeHigh.HigEq))
                        {
                            insertVariablePriorityPatient(template.Name, valueTmp, $"285@validateRangeHigher@>=@Column@{rangeHigh.Column}@value@{rangeHigh.HigEq}", row);
                        }

                        continue;
                    }

                    if (double.TryParse(valueTmp, out tmpValueDb))
                    {
                        if (tmpValueDb < Convert.ToDouble(rangeHigh.HigEq))
                        {
                            insertVariablePriorityPatient(template.Name, valueTmp, $"295@validateRangeHigher@>=@Column@{rangeHigh.Column}@value@{rangeHigh.HigEq}", row);
                        }

                        continue;
                    }

                    DateTime dateTimeTmp;

                    if (DateTime.TryParse(valueTmp, out dateTimeTmp))
                    {
                        if (dateTimeTmp < Convert.ToDateTime(rangeHigh.HigEq))
                        {
                            insertVariablePriorityPatient(template.Name, dateTimeTmp.ToShortDateString(), $"307@validateRangeHigher@>=@Column@{rangeHigh.Column}@value@{Convert.ToDateTime(rangeHigh.HigEq).ToShortDateString()}", row);
                        }
                    }

                }

            }
        }

        /// <summary>
        /// Este metodo es el encargado de validar que los valores de las columnas sean menor a un valor o
        /// a otra columna dependiento la regla configurada en el archivo JSON.
        /// </summary>
        /// <param name="template">Reglas de la columna</param>
        /// <param name="row">El registro o fila del archivo</param>
        /// <param name="value">EL valor de la columna*fila</param>
        private void validateRangeMin(TemplateRulesPriorityPatient template, IXLTableRow row, object value)
        {
            RangeMin[] lstRangeMin;

            if (template.LstRangeMin != null && template.LstRangeMin.Length > 0)
            {
                string valueTmp = value.ToString();

                lstRangeMin = template.LstRangeMin;

                double tmpValueInt;
                double tmpValueDb;

                foreach (var rangeMin in lstRangeMin)
                {
                    if (rangeMin.MinEq.Equals("sys"))
                    {
                        DateTime fecha = Convert.ToDateTime(valueTmp);

                        if (fecha > DateTime.Now)
                        {
                            //Insertar en la tabla de pacientes prioritarios
                            insertVariablePriorityPatient(template.Name, valueTmp, $"338@validateRangeMin@<=@Column@{rangeMin.Column}@value@{rangeMin.MinEq}", row);
                        }

                        continue;
                    }

                    string[] vals = rangeMin.MinEq.Split('|');

                    if (vals.Length > 0 && vals[0].Equals("column"))
                    {

                        if (double.TryParse(valueTmp, out tmpValueInt))
                        {
                            if (tmpValueInt > Convert.ToDouble(row.Cell(vals[1]).GetString()))
                            {
                                insertVariablePriorityPatient(template.Name, valueTmp, $"353@validateRangeMin@<=@Column@{vals[1]}@value@{row.Cell(vals[1]).GetString()}", row);
                            }

                            continue;
                        }


                        if (Convert.ToDateTime(valueTmp) > row.Cell(vals[1]).GetDateTime())
                        {
                            insertVariablePriorityPatient(template.Name, Convert.ToDateTime(valueTmp).ToShortDateString(), $"362@validateRangeMin@<=@Column@{vals[1]}@value@{row.Cell(vals[1]).GetString()}", row);//fecha pero no hay cambio, solo se muestra el valor en string, quizas porque no se sabe el tipo de dato...aunque se está haciendo la validación con una fecha.
                        }

                        continue;
                    }

                    if (rangeMin.Column != 0)
                    {
                        if (double.TryParse(valueTmp, out tmpValueInt) || double.TryParse(valueTmp, out tmpValueDb))
                        {
                            var s_s_s = rangeMin.Column;
                            var s_s = row.Cell(s_s_s).GetString();
                            var s = Convert.ToDouble(s_s);
                            var a_a = rangeMin.ValueColumnEq;
                            var a = Convert.ToDouble(a_a);
                            if (s != a)
                            {
                                insertVariablePriorityPatient(template.Name, valueTmp, $"374@validateRangeMin@==@Column@{rangeMin.Column}@value@{rangeMin.ValueColumnEq}", row);
                            }

                            continue;
                        }

                        if (Convert.ToDateTime(valueTmp) > Convert.ToDateTime(rangeMin.ValueColumnEq))
                        {
                            insertVariablePriorityPatient(template.Name, Convert.ToDateTime(valueTmp).ToShortDateString(), $"382@validateRangeMin@==@Column@{rangeMin.Column}@value@{Convert.ToDateTime(rangeMin.MinEq).ToShortDateString()}", row); //debería ser el valor == pero está el valor menor.
                        }

                        continue;
                    }

                    if (double.TryParse(valueTmp, out tmpValueInt))
                    {
                        double minEqtmp;

                        if (double.TryParse(rangeMin.MinEq, out minEqtmp))
                        {
                            if (tmpValueInt > minEqtmp)
                            {
                                insertVariablePriorityPatient(template.Name, valueTmp, $"396@validateRangeMin@>=@Column@{rangeMin.Column}@value@{rangeMin.MinEq}", row);
                            }

                            continue;
                        }

                        if (tmpValueInt > Convert.ToInt32(rangeMin.MinEq))
                        {
                            insertVariablePriorityPatient(template.Name, valueTmp, $"404@validateRangeMin@>=@Column@{rangeMin.Column}@value@{rangeMin.MinEq}", row);
                        }

                        continue;
                    }

                    if (double.TryParse(valueTmp, out tmpValueDb))
                    {
                        if (tmpValueDb > Convert.ToDouble(rangeMin.MinEq))
                        {
                            insertVariablePriorityPatient(template.Name, valueTmp, $"414@validateRangeMin@>=@Column@{rangeMin.Column}@value@{rangeMin.MinEq}", row);
                        }

                        continue;
                    }

                    DateTime dateTimeTmp;

                    if (DateTime.TryParse(valueTmp, out dateTimeTmp))
                    {

                        if (dateTimeTmp > Convert.ToDateTime(rangeMin.MinEq))
                        {
                            insertVariablePriorityPatient(template.Name, dateTimeTmp.ToShortDateString(), $"427@validateRangeMin@>=@Column@{rangeMin.Column}@value@{rangeMin.MinEq}", row);
                        }

                    }

                }
            }

        }
        /// <summary>
        /// Este metodo es el encargardo de validar que los valores de las columnas sean diferentes a un valor o
        /// a otra columna dependiento la regla configurada en el archivo JSON.
        /// </summary>
        /// <param name="template">Reglas de la columna</param>
        /// <param name="row">El registro o fila del archivo</param>
        /// <param name="value">EL valor de la columna*fila</param>
        private void validateRangeDif(TemplateRulesPriorityPatient template, IXLTableRow row, object value, string type)
        {
            RangeDif[] lstRangeDif;

            if (template.LstRangeDif != null && template.LstRangeDif.Length > 0)
            {
                lstRangeDif = template.LstRangeDif;

                foreach (var rangeDif in lstRangeDif)
                {
                    string valueTmp = Convert.ToString(value).Trim();

                    // Si el valor es diferente
                    if (!rangeDif.Dif.Equals(value))
                    {
                        // Si hay un valor para comparar si es igual
                        if (!rangeDif.ValueColumnEq.Equals("-1") && !rangeDif.ValueColumnEq.Equals(""))
                        {
                            // Se compara si es diferente con otra columna, si lo es, esta incorrecto, deberia ser igual
                            if (!row.Cell(rangeDif.Column).GetString().Trim().Equals(rangeDif.ValueColumnEq.Trim()))
                            {
                                //Insertar en la tabla de pacientes prioritarios
                                insertVariablePriorityPatient(template.Name, valueTmp, $"459@validateRangeDif@=@Column@{rangeDif.Column}@value@{rangeDif.ValueColumnEq}", row);
                            }
                        }
                        // Si hay un valor para comparar si es diferente
                        else if (!rangeDif.Different.Equals("-1") && !rangeDif.Different.Equals(""))
                        {
                            // Se compara si es igual con otra columna, si lo es, esta incorrecto, deberia ser diferente
                            if (row.Cell(rangeDif.Column).GetString().Trim().Equals(rangeDif.Different.Trim()))
                            {
                                //Insertar en la tabla de pacientes prioritarios
                                insertVariablePriorityPatient(template.Name, valueTmp, $"496@validateRangeDif@!=@Column@{rangeDif.Column}@value@{rangeDif.Different}", row);
                            }
                        }
                        // Si hay un valor para comparar si es menor o igual
                        else if (!rangeDif.MinEq.Equals("-1") && !rangeDif.MinEq.Equals(""))
                        {
                            double tmpValue;

                            if (double.TryParse(rangeDif.MinEq, out tmpValue))
                            {
                                // Se compara si es mayor con otra columna, si lo es, esta incorrecto, deberia ser menor o igual
                                if (Convert.ToDouble(row.Cell(rangeDif.Column).GetString()) > tmpValue)
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"483@validateRangeDif@<=@Column@{rangeDif.Column}@value@{tmpValue}", row);
                                }
                            }
                            else
                            {
                                // Se compara si es mayor con otra columna, si lo es, esta incorrecto, deberia ser menor o igual
                                if (row.Cell(rangeDif.Column).GetDateTime() > Convert.ToDateTime(rangeDif.MinEq))
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"492@validateRangeDif@<=@Column@{rangeDif.Column}@value@{rangeDif.MinEq}", row);
                                }
                            }
                        }
                        // Si hay un valor para comparar si es mayor o igual
                        else
                        {

                            double tmpValue;

                            if (double.TryParse(rangeDif.HigEq, out tmpValue))
                            {
                                // Se compara si es menor con otra columna, si lo es, esta incorrecto, deberia ser mayor o igual
                                if (Convert.ToDouble(row.Cell(rangeDif.Column).GetString()) < tmpValue)
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"508@validateRangeDif@>=@Column@{rangeDif.Column}@value@{tmpValue}", row);
                                }
                            }
                            else
                            {
                                // Se compara si es menor con otra columna, si lo es, esta incorrecto, deberia ser mayor o igual
                                if (row.Cell(rangeDif.Column).GetDateTime() < Convert.ToDateTime(rangeDif.HigEq))
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"517@validateRangeDif@>=@Column@{rangeDif.Column}@value@{rangeDif.HigEq}", row);
                                }
                            }

                        }

                    }
                }

            }
        }

        /// <summary>
        /// Este metodo es el encargardo de validar que los valores de las columnas sean iguales a un valor o
        /// a otra columna dependiento la regla configurada en el archivo JSON.
        /// </summary>
        /// <param name="template">Reglas de la columna</param>
        /// <param name="row">El registro o fila del archivo</param>
        /// <param name="value">EL valor de la columna*fila</param>
        private void validateRangeEq(TemplateRulesPriorityPatient template, IXLTableRow row, object value, string type)
        {
            RangeEq[] lstRangeEq;

            if (template.LstRangeEq != null && template.LstRangeEq.Length > 0)
            {
                lstRangeEq = template.LstRangeEq;

                foreach (var rangeEq in lstRangeEq)
                {

                    string valueTmp = Convert.ToString(value).Trim();

                    // Si el valor es igual al del campo

                    if (rangeEq.Equal.Equals(valueTmp))
                    {
                        // Si hay un valor para comparar si es igual
                        if (rangeEq.ValueColumnEq != null && !rangeEq.ValueColumnEq.Equals("-1") && !rangeEq.ValueColumnEq.Equals(""))
                        {
                            var s = row.Cell(rangeEq.Column);
                            // Se compara si es diferente con otra columna, si lo es, esta incorrecto, deberia ser igual
                            if (!row.Cell(rangeEq.Column).GetString().Trim().Equals(rangeEq.ValueColumnEq.Trim()))
                            {
                                //Insertar en la tabla de pacientes prioritarios
                                insertVariablePriorityPatient(template.Name, valueTmp, $"553@validateRangeEq@=@Column@{rangeEq.Column}@value@{rangeEq.ValueColumnEq}", row);
                            }
                        }
                        // Si hay un valor para comparar si es diferente
                        else if (rangeEq.Different != null && !rangeEq.Different.Equals("-1") && !rangeEq.Different.Equals(""))
                        {
                            // Se compara si es igual con otra columna, si lo es, esta incorrecto, deberia ser diferente
                            if (row.Cell(rangeEq.Column).GetString().Trim().Equals(rangeEq.Different.Trim()))
                            {
                                //Insertar en la tabla de pacientes prioritarios
                                insertVariablePriorityPatient(template.Name, valueTmp, $"563@validateRangeEq@<>@Column@{rangeEq.Column}@value@{rangeEq.Different}", row);
                            }
                        }
                        // Si hay un valor para comparar si es menor o igual
                        else if (rangeEq.MinEq != null && !rangeEq.MinEq.Equals("-1") && !rangeEq.MinEq.Equals(""))
                        {
                            double tmpValue;

                            if (double.TryParse(rangeEq.MinEq, out tmpValue))
                            {
                                double valColumnDbTmp;

                                if (double.TryParse(row.Cell(rangeEq.Column).GetString(), out valColumnDbTmp))
                                {
                                    // Se compara si es mayor con otra columna, si lo es, esta incorrecto, deberia ser menor o igual
                                    if (valColumnDbTmp > tmpValue)
                                    {
                                        //Insertar en la tabla de pacientes prioritarios
                                        insertVariablePriorityPatient(template.Name, valueTmp, $"581@validateRangeEq@>=@Column@{rangeEq.Column}@value@{tmpValue}", row);
                                    }

                                    continue;
                                }

                                // Se compara si es mayor con otra columna, si lo es, esta incorrecto, deberia ser menor o igual
                                if (Convert.ToDouble(row.Cell(rangeEq.Column).GetString()) > tmpValue)
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"591@validateRangeEq@>=@Column@{rangeEq.Column}@value@{tmpValue}", row);
                                }
                            }
                            else
                            {
                                // Se compara si es mayor con otra columna, si lo es, esta incorrecto, deberia ser menor o igual
                                if (row.Cell(rangeEq.Column).GetDateTime() > Convert.ToDateTime(rangeEq.MinEq))
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"600@validateRangeEq@>=@Column@{rangeEq.Column}@value@{rangeEq.MinEq}", row);
                                }
                            }
                        }
                        // Si hay un valor para comparar si es mayor o igual
                        else
                        {

                            double tmpValue;

                            if (rangeEq.HigEq != null && double.TryParse(rangeEq.HigEq, out tmpValue))
                            {
                                string dataTmp = row.Cell(rangeEq.Column).GetString();

                                double valColumnIntTmp;
                                double valColumnDbTmp;

                                if (double.TryParse(dataTmp, out valColumnIntTmp))
                                {
                                    // Se compara si es menor con otra columna, si lo es, esta incorrecto, deberia ser mayor o igual
                                    if (valColumnIntTmp < tmpValue)
                                    {
                                        //Insertar en la tabla de pacientes prioritarios
                                        insertVariablePriorityPatient(template.Name, valueTmp, $"623@validateRangeEq@>=@Column@{rangeEq.Column}@value@{tmpValue}", row);
                                    }
                                    continue;
                                }

                                if (double.TryParse(dataTmp, out valColumnDbTmp))
                                {
                                    // Se compara si es menor con otra columna, si lo es, esta incorrecto, deberia ser mayor o igual
                                    if (valColumnDbTmp < tmpValue)
                                    {
                                        //Insertar en la tabla de pacientes prioritarios
                                        insertVariablePriorityPatient(template.Name, valueTmp, $"634@validateRangeEq@>=@Column@{rangeEq.Column}@value@{tmpValue}", row);
                                    }
                                    continue;
                                }

                            }
                            else
                            {
                                // Se compara si es menor con otra columna, si lo es, esta incorrecto, deberia ser mayor o igual
                                if (row.Cell(rangeEq.Column).GetDateTime() < Convert.ToDateTime(rangeEq.HigEq))
                                {
                                    //Insertar en la tabla de pacientes prioritarios
                                    insertVariablePriorityPatient(template.Name, valueTmp, $"646@validateRangeEq@>=@Column@{rangeEq.Column}@value@{rangeEq.HigEq}", row);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Permite insertar el paciente prioritario
        /// </summary>
        /// <param name="row">Fila del archivo procesada</param>
        #region Add Data

        private tbl_paciente_prioritario insertPriorityPatient(IXLTableRow row)
        {
            try
            {
                tbl_paciente_prioritario currentPatient;
                int rownumber = row.RowNumber();
                var tempKeyPair = wrapper.GetValue(rownumber);
                if (tempKeyPair == null)
                {
                    currentPatient = new tbl_paciente_prioritario
                    {
                        id = Guid.NewGuid(),
                        apellidos = $"{row.Field("Primer_Apellido").Value.ToString()} {row.Field("Segundo_Apellido").Value.ToString()}",
                        cedula = $"{row.Field("Tipo_Identificacion").Value.ToString()} {row.Field("Numero_Identificacion").Value.ToString()}",
                        nombres = $"{row.Field("Primer_Nombre").Value.ToString()} {row.Field("Segundo_Nombre").Value.ToString()}",
                        numContacto = $"{row.Field("Numero_Telefonico").Value.ToString()}",
                        idArchivo = Guid.Parse(idArchivo)
                    };
                }
                else
                {
                    currentPatient = tempKeyPair;
                }
                wrapper.Add(row.RowNumber(), currentPatient);
                return currentPatient;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
            }
            return null;
        }

        /// <summary>
        /// Inserta la variable prioritaria que no cumple alguna de las reglas
        /// </summary>
        /// <param name="variableName">Nombre de las variables</param>
        /// <param name="variableValue">Valor de la variable</param>
        /// <param name="motnhOutdated">Meses que la variable esta desactualizada</param>
        /// <param name="row">Fila procesada</param>
        private void insertOutdated(string variableName, string variableValue, string motnhOutdated, IXLTableRow row)
        {
            try
            {
                int rownumber = row.RowNumber();
                tbl_paciente_prioritario currentPatient = insertPriorityPatient(row);

                tbl_variable_desactualizada variable = new tbl_variable_desactualizada()
                {
                    Id = Guid.NewGuid(),
                    nombreVariable = variableName,
                    valorVariable = variableValue,
                    mesesDesactualizado = motnhOutdated.ToString(),
                    tbl_paciente_prioritario_id = currentPatient.id
                };
                variable.tbl_paciente_prioritario = currentPatient;

                currentPatient.tbl_variable_desactualizada.Add(variable);
                wrapper.Add(rownumber, currentPatient);

                desactualizado.Add(variable);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
            }
            
        }

        private void insertVariablePriorityPatient(string variableName, string variableValue, string umbralValue, IXLTableRow row)
        {
            try
            {
                int rownumber = row.RowNumber();
                tbl_paciente_prioritario currentPatient = insertPriorityPatient(row);
                
                tbl_variable_prioritaria variable = new tbl_variable_prioritaria()
                {
                    Id = Guid.NewGuid(),
                    nombreVariable = variableName,
                    valorVariable = variableValue,
                    valorUmbral = umbralValue,
                    tbl_paciente_prioritario_id = currentPatient.id
                };

                variable.tbl_paciente_prioritario = currentPatient;
                currentPatient.tbl_variable_prioritaria.Add(variable);
                wrapper.Add(rownumber, currentPatient);

                variable_prioritaria.Add(variable);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
            }
        }
        
        private bool SaveData()
        {
            try
            {
                var db = SingletonDB.getInstance().DB;

                var dir = wrapper.GetDictionary();
                int des = 0, vas = 0;
                dir.ForEach(m => des += m.Value.tbl_variable_desactualizada.Count());
                dir.ForEach(m => vas += m.Value.tbl_variable_prioritaria.Count());
                int total = des + vas + dir.Count();
                wrapper = new WrapperObject<string, int, tbl_paciente_prioritario>();
                int saved = 0;
                foreach (var item in dir)
                {
                    try
                    {
                        db.tbl_paciente_prioritario.Add(item.Value);
                        saved += db.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
                return false;
            }
            return true;
        }
        #endregion
        /// <summary>
        /// Carga el archivo de reglas JSON para validar los registros del Excel
        /// </summary>
        /// <returns>El archivo JSON en objetos</returns>
        private List<TemplateRulesPriorityPatient> GetTemplate()
        {
            var cac_template = Configuration.GetValueConf(Constants.PRIORITY_PATIENT_TEMPLATE);
            var default_path = IOUtilities.GetDefaultPath(@"", FileAttributes.Normal);
            var local_path = string.Format(@"{0}{1}", default_path, cac_template);
            string json = IOUtilities.ReadAllText(local_path);
            if (json != "")
            {
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                var o = json_serializer.Deserialize<TempRulesPriorityPatientCollection>(json);
                List<TemplateRulesPriorityPatient> template = o.collection.ToList();
                return template;
            }
            else
            {
                return new List<TemplateRulesPriorityPatient>();
            }
        }
    }
}
