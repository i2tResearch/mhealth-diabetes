using CAC.Library.Model.DTO;
using CAC.Library.Model.POCO;
using CAC.Library.Utilities;
using ClosedXML.Excel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CAC.Library.BP
{
    public class FileProcessBP : IFileProcessBP
    {
        private int totalRow = 0;

        public int GetTotalRow()
        {
            return totalRow;
        }

        private ValidatorResult<DTOValidacionArchivo> validator_result = new ValidatorResult<DTOValidacionArchivo>();

        private IConvertFormat converter = new ConvertFormat();

        private List<TemplateFormatCAC> listTemplateFormatCAC = new List<TemplateFormatCAC>();

        private DTOValidacionArchivo ExceptionWriter(Exception ex)
        {
            DTOValidacionArchivo validation = new DTOValidacionArchivo()
            {
                Valor = ex.StackTrace,
                FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                Descripcion = string.Format(Resource_DefaultMessage.ERROR_LOG, ex.Message, ""),
                Celda = "A1",
                Fila = "1",
                Columna = "A"
            };
            return validation;
        }
        
        public List<TemplateFormatCAC> GetTemplate()
        {
            try
            {
                if (listTemplateFormatCAC.Count == 0)
                {
                    var cac_template = Configuration.GetValueConf(Constants.CAC_TEMPLATE);
                    var default_path = IOUtilities.GetDefaultPath(@"", FileAttributes.Normal);
                    var local_path = string.Format(@"{0}{1}", default_path, cac_template);
                    string json = IOUtilities.ReadAllText(local_path);

                    if (json != null && json != "")
                    {
                        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                        var o = json_serializer.Deserialize<List<TemplateFormatCAC>>(json);
                        List<TemplateFormatCAC> template = o.ToList();
                        cac_template = default_path = local_path = json = null;
                        json_serializer = null;
                        o = null;
                        listTemplateFormatCAC = template;
                        return template;
                    }
                }
                else
                {
                    return listTemplateFormatCAC;
                }
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
                throw ex;
            }
            return new List<TemplateFormatCAC>();
        }

        public List<DTOValidacionArchivo> ValidateFile(string nameFile, byte[] binaryFile)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<DTOValidacionArchivo> lstValidaciones = new List<DTOValidacionArchivo>();
            try
            {
                Build_Workbook(nameFile, binaryFile);
                lstValidaciones.AddRange(validator_result.GetResult());
            }
            catch (Exception ex)
            {
                lstValidaciones.Add(ExceptionWriter(ex));
                throw ex;
            }
            var rs = lstValidaciones.OrderBy(m => m.Columna).ToList();
            stopwatch.Stop();
            //Auditor.SaveAnalysis(stopwatch.Elapsed.TotalMinutes, this.validator_result);
            return rs;
        }
        #region Builders
        private void Build_Workbook(string nameFile, byte[] binaryFile)
        {
            try
            {
                if (nameFile.Contains(".csv") == true)
                {
                    var lines = converter.ReadCSV(binaryFile, ',').ToList();
                    var number_rows = lines.Count();
                    int boundary = validator_result.Limit;
                    if (number_rows > boundary)
                    {
                        #region Multi Tasking Validator
                        int count = number_rows / boundary;
                        int range = boundary;
                        var tasks = new List<Task>();
                        for (int i = 0; i < count + 1; i++)
                        {
                            var sublist = new List<string[]>();
                            int top = boundary, bottom = 0;
                            string[] headerrow = lines[0];
                            if (i > 0)
                            {
                                top = (range + boundary) >= lines.Count() ? (lines.Count() - 1) - range : boundary;
                                bottom = (range + 1) > (lines.Count() - 1) ? lines.Count() - 1 : range + 1;
                                range += top;
                                sublist.Add(headerrow);
                            }
                            sublist.AddRange(lines.GetRange(bottom, top));
                            Task task1 = Task.Factory.StartNew(() => Build_Worksheet($"{range}", sublist));
                            tasks.Add(task1);
                            //if (i % 3 == 0)
                            //{
                            //    Task.WaitAll(tasks.ToArray());
                            //    Auditor.SaveAnalysis(0, validator_result);
                            //    int total = validator_result.Totalrow;
                            //    validator_result = new ValidatorResult<DTOValidacionArchivo>();
                            //}
                            //Auditor.SaveLog($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}\tRango {range}\tTotal {lines.Count()}\tValidaciones {result.GetResult().Count}.");
                        }
                        Task.WaitAll(tasks.ToArray());
                        var exception = tasks.Exists(m => m.Exception != null || m.Status == TaskStatus.Faulted || m.Status == TaskStatus.Canceled);
                        if(exception == true)
                        {
                            string cause = "";
                            tasks.Where(m => m.Exception != null || m.Status == TaskStatus.Faulted || m.Status == TaskStatus.Canceled).ToList().ForEach(m => cause = $"Task ID:\t{m.Id}\tStatus:\t{m.Status}@{cause}");
                            throw new Exception(cause);
                        }
                        #endregion
                    }
                    else
                    {
                        Build_Worksheet($"{lines.Count()}", lines);
                    }

                    #region Close and Delete
                    
                    lines = null;
                    number_rows = 0;
                    boundary = 0;
                    #endregion
                }
                else if (nameFile.Contains(".xls") == true || nameFile.Contains(".xlsx"))
                {
                    using (Stream streamFile = new MemoryStream(binaryFile))
                    {
                        var wb = new XLWorkbook(streamFile);
                        Validator(wb);
                    }
                       
                }
                else
                {
                    DTOValidacionArchivo validation = new DTOValidacionArchivo()
                    {
                        FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                        Descripcion = string.Format(Resource_DefaultMessage.ERROR_FILE_NOT_VALID_EXT, nameFile),
                        Valor = $"{nameFile}",
                        Celda = $"A1",
                        Fila = "1",
                        Columna = "A"
                    };
                    Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Build_Worksheet), validation.Columna, validation.Valor, "ERROR_FILE_NOT_VALID_EXT", validation.ToString(), "Sheet1"));
                    validator_result.Add(validation);
                }
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
                throw ex;
            }
        }

        private void Build_Worksheet(string worksheetName, IEnumerable<string[]> csvLines)
        {
            try
            {
                DTOValidacionArchivo validation;
                using (XLWorkbook workbook = converter.Convert_CSV_ClosedXML(worksheetName, csvLines))
                {
                    if (workbook != null)
                    {
                        Validator(workbook);
                    }
                    else
                    {
                        validation = new DTOValidacionArchivo()
                        {
                            FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                            Descripcion = string.Format(Resource_DefaultMessage.ERROR_FILE_NOT_VALID_EXT, worksheetName),
                            Valor = $"{worksheetName}",
                            Celda = $"A1",
                            Fila = "1",
                            Columna = "A"
                        };
                        Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Build_Worksheet), validation.Columna, validation.Valor, "ERROR_FILE_NOT_VALID_EXT", validation.ToString(), worksheetName));
                        validator_result.Add(validation);
                    }
                }
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
                throw ex;
            }
        }
        #endregion

        #region Validators
        public void Validator(XLWorkbook workBookCAC)
        {
            try
            {
                using (workBookCAC)
                {


                    DTOValidacionArchivo validation;
                    List<TemplateFormatCAC> templateCAC = new List<TemplateFormatCAC>();
                    templateCAC = this.GetTemplate();
                    if (templateCAC != null && templateCAC.Count > 0)
                    {
                        using (var worksheet = workBookCAC.Worksheet(1))
                        {
                            var range = worksheet.Range(worksheet.FirstCellUsed().Address, worksheet.LastCellUsed().Address);
                            //Validacion del columnas repetidas.
                            List<string> nombres = new List<string>();
                            range.ColumnsUsed().ToList().ForEach(m => nombres.Add(m.Cell(1).Value.ToString()));
                            bool duplicates = nombres.HasDuplicates();
                            if (duplicates == true)
                            {
                                var listduplicates = nombres.GroupBy(s => s).SelectMany(grp => grp.Skip(1));
                                string concatDuplicate = "{0}\t";
                                listduplicates.ForEach(m => concatDuplicate = string.Format(concatDuplicate, m));
                                validation = new DTOValidacionArchivo()
                                {
                                    FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                    Descripcion = string.Format(Resource_DefaultMessage.ERROR_COLUMN_NAME_REPEATED, listduplicates.Count(), concatDuplicate),
                                    Valor = concatDuplicate,
                                    Celda = $"{range.RangeAddress.FirstAddress}:{range.RangeAddress.LastAddress}",
                                    Fila = "1",
                                    Columna = "A"
                                };
                                Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator), validation.Columna, validation.Valor, "ERROR_COLUMN_NAME_REPEATED", validation.ToString(), worksheet.Name));
                                validator_result.Add(validation);
                                nombres = null;
                            }
                            //else
                            //{

                            //}
                            using (var table = range.AsTable())
                            {
                                validator_result.Totalrow += worksheet.RowsUsed().Count();
                                this.totalRow += table.DataRange.RowCount();
                                Validator_Row(templateCAC, table);
                                Validator_Column(templateCAC, table);
                                Task t1 = Task.Factory.StartNew(() => Validator_Row(templateCAC, table));
                                Task t2 = Task.Factory.StartNew(() => Validator_Column(templateCAC, table));
                                Task.WaitAll(t1, t2);
                                //Task.Factory.StartNew(() => Validator_Row(templateCAC, table));
                                //Task.Factory.StartNew(() => Validator_Column(templateCAC, table));
                            }
                        }
                    }
                    else
                    {
                        validation = new DTOValidacionArchivo()
                        {
                            Valor = $"{Configuration.GetValueConf(Constants.CAC_TEMPLATE)}",
                            FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                            Descripcion = Resource_DefaultMessage.ERROR_TEMPLATE_NOT_FOUND,
                            Celda = $"A1",
                            Fila = "1",
                            Columna = "A"
                        };
                        Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator), validation.Columna, validation.Valor, "ERROR_TEMPLATE_NOT_FOUND", validation.ToString(), workBookCAC.Worksheet(1).Name));
                        validator_result.Add(validation);
                    }
                }
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
            }
        }
        
        private void Validator_Row(List<TemplateFormatCAC> templateCAC, IXLTable table)
        {
            try
            {
                using (table)
                {
                    DTOValidacionArchivo validation;
                    int header_index = table.HeadersRow().RowNumber();
                    int rowCount = table.RowsUsed().Count();
                    for (int row_index = 1; row_index <= rowCount; row_index++)
                    {
                        var row = table.Row(row_index);
                        if (row_index != header_index)
                        {
                            bool existErrors = false;
                            row.DeleteComments();
                            row.Clear(XLClearOptions.Formats);
                            foreach (var cell in row.Cells())
                            {
                                int columnNumber = cell.Address.ColumnNumber - 1 > 0 ? (cell.Address.ColumnNumber - 1) : 0;
                                var field = table.Fields.Where(m => m.Index == columnNumber).FirstOrDefault();
                                var headerrow = field != null ? templateCAC.Where(m => m.Name == field.Name).FirstOrDefault() : null;
                                if (headerrow != null)
                                {
                                    bool response = Validator_Cell(headerrow, cell);
                                    if (response)
                                        existErrors = response;
                                }
                                else
                                {
                                    var fieldname = field != null ? field.Name : "";
                                    validation = new DTOValidacionArchivo()
                                    {
                                        FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                        Descripcion = string.Format(Resource_DefaultMessage.ERROR_COLUMN_NOT_ALLOWED, fieldname),
                                        Valor = cell.Value.ToString(),
                                        Celda = $"{cell.Address.ColumnLetter}{cell.Address.RowNumber}",
                                        Fila = $"{row_index}",
                                        Columna = $"{cell.Address.ColumnLetter}"
                                    };
                                    Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Row), validation.Columna, validation.Valor, "ERROR_COLUMN_NOT_ALLOWED", validation.ToString(), table.Worksheet.Name));
                                    validator_result.Add(validation);
                                }
                            }
                            if (existErrors == false)
                            {
                                Auditor.SaveLog(null, table.Worksheet.Name, row_index, row);
                            }
                            existErrors = false;
                        }
                        Auditor.SaveLog("true", table.Worksheet.Name, row_index, row);
                    }
                }
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
                throw ex;
            }
        }
        /// <summary>
        /// Return true if it have at least 1 DTOValidacionArchivo object instance. Q: Have any format error? YES(TRUE) or NO(FALSE)
        /// </summary>
        /// <param name="template">Template format object</param>
        /// <param name="cell">IXL Cell objet</param>
        /// <returns>True for any new instance DTOValidacionArchivo. If DTOValidacionArchivo is NULL then return false</returns>
        private bool Validator_Cell(TemplateFormatCAC template, IXLCell cell)
        {
            bool flag = false;
            try
            {
                DTOValidacionArchivo validation = null;
                
                #region Validacion si es null
                if (cell.IsEmpty() == true && template.Nullable == false)
                {
                    validation = new DTOValidacionArchivo()
                    {
                        FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                        Descripcion = string.Format(Resource_DefaultMessage.ERROR_CELL_EMPTY_OR_NULL, cell.WorksheetColumn().ColumnLetter(), cell.WorksheetRow().RowNumber(), template.Name),
                        Valor = cell.GetString(),
                        Celda = $"{cell.WorksheetColumn().ColumnLetter()}{cell.WorksheetRow().RowNumber()}",
                        Fila = $"{cell.WorksheetRow().RowNumber()}",
                        Columna = $"{cell.WorksheetColumn().ColumnLetter()}"
                    };
                    Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Cell), validation.Columna, validation.Valor, "ERROR_CELL_EMPTY_OR_NULL", validation.ToString(), cell.Worksheet.Name));
                    validator_result.Add(validation);
                }
                #endregion
                #region Si no es null or empty
                else if (cell.IsEmpty() == false)
                {
                    bool hasFormula = cell.HasFormula;
                    if (hasFormula == true)
                    {
                        #region Si tiene formula
                        validation = new DTOValidacionArchivo()
                        {
                            FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                            Descripcion = string.Format(Resource_DefaultMessage.ERROR_CELL_HAVE_FORMULA, cell.WorksheetColumn().ColumnLetter(), cell.WorksheetRow().RowNumber(), template.Name),
                            Valor = cell.GetString(),
                            Celda = $"{cell.WorksheetColumn().ColumnLetter()}{cell.WorksheetRow().RowNumber()}",
                            Fila = $"{cell.WorksheetRow().RowNumber()}",
                            Columna = $"{cell.WorksheetColumn().ColumnLetter()}"
                        };
                        Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Cell), validation.Columna, validation.Valor, "ERROR_CELL_HAVE_FORMULA", validation.ToString(), cell.Worksheet.Name));
                        validator_result.Add(validation);
                        #endregion Si tiene formula
                    }
                    else
                    {
                        #region Validacion del tipo de dato
                        var cell_datatype = cell.DataType.ToString();
                        if (template.Type.Contains(cell_datatype) == false)
                        {
                            validation = new DTOValidacionArchivo()
                            {
                                FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                Descripcion = string.Format(Resource_DefaultMessage.ERROR_CELL_NOT_VALID_TYPE, cell.WorksheetColumn().ColumnLetter(), cell.WorksheetRow().RowNumber(), template.Type, cell_datatype, cell.Value, template.Name),
                                Valor = cell.GetString(),
                                Celda = $"{cell.WorksheetColumn().ColumnLetter()}{cell.WorksheetRow().RowNumber()}",
                                Fila = $"{cell.WorksheetRow().RowNumber()}",
                                Columna = $"{cell.WorksheetColumn().ColumnLetter()}"
                            };
                            Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Cell), validation.Columna, validation.Valor, "ERROR_CELL_NOT_VALID_TYPE", validation.ToString(), cell.Worksheet.Name));
                            validator_result.Add(validation);
                        }
                        #endregion
                        #region Validacion si el valor está contenido en la lista predeterminado y si es texto
                        if (template.SelectList != null && template.SelectList.Count > 0)
                        {
                            bool isContained = false;
                            try
                            {
                                isContained = template.SelectList.Where(m => m.Value == cell.GetValue<string>()).Count() > 0 ? true : false;
                            }
                            catch (Exception)
                            {
                                isContained = false;
                            }

                            if (isContained == false)
                            {
                                validation = new DTOValidacionArchivo()
                                {
                                    FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                    Descripcion = string.Format(Resource_DefaultMessage.ERROR_CELL_DO_NOT_LIST_VALUE, cell.WorksheetColumn().ColumnLetter(), cell.WorksheetRow().RowNumber(), cell.Value, template.Name),
                                    Valor = cell.GetString(),
                                    Celda = $"{cell.WorksheetColumn().ColumnLetter()}{cell.WorksheetRow().RowNumber()}",
                                    Fila = $"{cell.WorksheetRow().RowNumber()}",
                                    Columna = $"{cell.WorksheetColumn().ColumnLetter()}"
                                };
                                Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Cell), validation.Columna, validation.Valor, "ERROR_CELL_DO_NOT_LIST_VALUE", validation.ToString(), cell.Worksheet.Name));
                                validator_result.Add(validation);
                            }
                        }
                        #endregion
                        #region Si es fecha y obtener el valor segun el tipo de dato
                        var cell_type = cell.Value.GetType();
                        if (typeof(DateTime).ToString().Contains(cell_type.ToString()) == true || template.Type.Contains(typeof(DateTime).Name))
                        {
                            bool regexResult = false;
                            try
                            {
                                DateTime cell_datetime;

                                if (cell.TryGetValue<DateTime>(out cell_datetime) == true)
                                {
                                    string datetime_to_comparer = cell_datetime.ToString(template.Format);
                                    string regex = @"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$";

                                    regexResult = Regex.Match(datetime_to_comparer, regex).Success;
                                }
                                else
                                {
                                    regexResult = false;
                                }
                            }
                            catch (Exception)
                            {
                                regexResult = false;
                            }
                            if (regexResult == false)
                            {
                                validation = new DTOValidacionArchivo()
                                {
                                    FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                    Descripcion = string.Format(Resource_DefaultMessage.ERROR_CELL_NOT_FORMAT, cell.WorksheetColumn().ColumnLetter(), cell.WorksheetRow().RowNumber(), template.Format, template.Name),
                                    Valor = cell.GetString(),
                                    Celda = $"{cell.WorksheetColumn().ColumnLetter()}{cell.WorksheetRow().RowNumber()}",
                                    Fila = $"{cell.WorksheetRow().RowNumber()}",
                                    Columna = $"{cell.WorksheetColumn().ColumnLetter()}"
                                };
                                Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Cell), validation.Columna, validation.Valor, "ERROR_CELL_NOT_FORMAT", validation.ToString(), cell.Worksheet.Name));
                                validator_result.Add(validation);
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                flag = validation == null ? false : true;
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
                flag = true;
                throw ex;
            }
            template = null;
            cell = null;
            return flag;
        }

        private void Validator_Column(List<TemplateFormatCAC> templateCAC, IXLTable table)
        {
            try
            {
                using (table)
                {
                    DTOValidacionArchivo validation = null;
                    foreach (TemplateFormatCAC template in templateCAC)
                    {
                        // Si el numero de columnas del formato es diferente a las del archivo
                        if (templateCAC.Count != table.HeadersRow().CellCount())
                        {
                            string concatHeader = "";
                            table.HeadersRow().CellsUsed().Select(m => m.Value.ToString()).ToList().Except(templateCAC.Select(m => m.Name).ToList()).ForEach(m => concatHeader = $"{concatHeader} {m}");
                            //table.HeadersRow().CellsUsed().ForEach(m => concatHeader = $"{concatHeader} {m.Value}");
                            validation = new DTOValidacionArchivo()
                            {
                                FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                Descripcion = string.Format(Resource_DefaultMessage.ERROR_COLUMNS_NOT_EQUALS, table.HeadersRow().CellCount(), templateCAC.Count),
                                Valor = $"{concatHeader}",
                                Celda = $"{table.HeadersRow().RangeAddress}",
                                Fila = $"{table.HeadersRow().RangeAddress.FirstAddress.RowNumber}",
                                Columna = $"{table.HeadersRow().RangeAddress.LastAddress.ColumnLetter}"
                            };
                            Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Column), validation.Columna, validation.Valor, "ERROR_COLUMNS_NOT_EQUALS", validation.ToString(), table.Worksheet.Name));
                            validator_result.Add(validation);
                        }
                        var field = table.Fields.Where(m => m.Name == template.Name).FirstOrDefault();
                        //Validacion de existencia de columna
                        if (field == null)
                        {
                            validation = new DTOValidacionArchivo()
                            {
                                FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                Descripcion = string.Format(Resource_DefaultMessage.ERROR_COLUMN_MISSING_NAME, template.Name),
                                Valor = template.Name,
                                Celda = $"A1",
                                Fila = $"1",
                                Columna = $"A"
                            };
                            Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Column), validation.Columna, validation.Valor, "ERROR_COLUMN_MISSING_NAME", validation.ToString(), table.Worksheet.Name));
                            validator_result.Add(validation);
                        }
                        else
                        {
                            int field_index = field.Index + 1;
                            IXLCells cells = table.Column(field_index).CellsUsed();
                            var values = cells.Select(m => m.Value).ToList();
                            values.RemoveAll(item => item == null || item.ToString() == "");
                            var duplicateKeys = values.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key);

                            //Validacion duplicados
                            bool hasDuplicates = template.Duplicate == false && duplicateKeys.Count() > 0;

                            //Validación de existencia de filas
                            if (table.Column(field_index).CellsUsed().Count() == 0)
                            {
                                var first = cells.OrderBy(m => m.Address).FirstOrDefault() != null ? cells.OrderBy(m => m.Address).FirstOrDefault().Address.ToString() : "";
                                var last = cells.OrderByDescending(m => m.Address).FirstOrDefault() != null ? cells.OrderByDescending(m => m.Address).FirstOrDefault().Address.ToString() : "";

                                validation = new DTOValidacionArchivo()
                                {
                                    FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                    Descripcion = string.Format(Resource_DefaultMessage.ERROR_COLUMN_NOT_DATA, template.Name),
                                    Valor = "",
                                    Celda = $"{first}:{last}",
                                    Fila = $"{table.HeadersRow().RangeAddress.FirstAddress.RowNumber}",
                                    Columna = $"{table.HeadersRow().RangeAddress.LastAddress.ColumnLetter}"
                                };
                                Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Column), validation.Columna, validation.Valor, "ERROR_COLUMN_NOT_DATA", validation.ToString(), table.Worksheet.Name));
                                validator_result.Add(validation);
                            }

                            if (hasDuplicates)
                            {
                                validation = new DTOValidacionArchivo()
                                {

                                    FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                    Descripcion = string.Format(Resource_DefaultMessage.ERROR_COLUMN_NOT_DUPLICATE_DATA, template.Name),
                                    Valor = "",
                                    Celda = $"{table.Column(field.Name).RangeAddress}",
                                    Fila = $"{table.Column(field.Name).RangeAddress.FirstAddress.RowNumber}",
                                    Columna = $"{table.Column(field.Name).RangeAddress.LastAddress.ColumnLetter}"
                                };
                                Auditor.SaveLog(string.Format(Resource_DefaultMessage.CONTROL_VALUE, nameof(FileProcessBP.Validator_Column), validation.Columna, validation.Valor, "ERROR_COLUMN_NOT_DUPLICATE_DATA", validation.ToString(), table.Worksheet.Name));
                                validator_result.Add(validation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validator_result.Add(ExceptionWriter(ex));
                throw ex;
            }
        }
        #endregion
    }

    public class ValidatorResult<T>
    {
        private int totalrow = 0;

        private int limit = 1000;
        public int Limit
        {
            get
            {
                return limit;
            }

            set
            {
                limit = value;
            }
        }

        private ConcurrentBag<T> concurrentbag = new ConcurrentBag<T>();

        public int Totalrow
        {
            get
            {
                return totalrow;
            }

            set
            {
                totalrow = value;
            }
        }

        public List<T> GetResult()
        {
            return concurrentbag.ToList<T>();
        }

        public void Add(T obj)
        {
            concurrentbag.Add(obj);
        }
    }

    public interface IConvertFormat
    {
        IEnumerable<string[]> ReadCSV(byte[] binaryFile, char delimiter = ';');

        IEnumerable<string[]> ReadCSV(string fileName, char delimiter = ';');

        XLWorkbook Convert_CSV_ClosedXML(string worksheetName, IEnumerable<string[]> csvLines);

        XLWorkbook Convert_CSV_ClosedXML(string worksheetName, string fileName, char delimiter = ';');

        XLWorkbook Convert_CSV_ClosedXML(string worksheetName, byte[] binaryFile, char delimiter = ';');
    }

    public class ConvertFormat: IConvertFormat
    {
        public IEnumerable<string[]> ReadCSV(byte[] binaryFile, char delimiter = ';')
        {
            using (StreamReader sr = new StreamReader(new MemoryStream(binaryFile), Encoding.UTF8))
            {
                List<string[]> lines = new List<string[]>();
                while (sr.EndOfStream == false)
                {
                    var line = sr.ReadLine().Split(delimiter);
                    lines.Add(line);
                }
                return lines.ToArray().AsEnumerable();
            }
        }

        public IEnumerable<string[]> ReadCSV(string fileName, char delimiter = ';')
        {
            var lines = System.IO.File.ReadAllLines(fileName, Encoding.UTF8).Select(a => a.Split(delimiter));
            return (lines);
        }

        public XLWorkbook Convert_CSV_ClosedXML(string worksheetName, byte[] binaryFile, char delimiter = ';')
        {
            var lines = ReadCSV(binaryFile, delimiter);
            var workbook = Convert_CSV_ClosedXML(worksheetName, lines);
            return workbook;
        }

        public XLWorkbook Convert_CSV_ClosedXML(string worksheetName, string fileName, char delimiter = ';')
        {
            var lines = ReadCSV(fileName, delimiter);
            var workbook = Convert_CSV_ClosedXML(worksheetName, lines);
            return workbook;
        }

        public XLWorkbook Convert_CSV_ClosedXML(string worksheetName, IEnumerable<string[]> csvLines)
        {
            if (csvLines == null || csvLines.Count() == 0)
            {
                return null;
            }
            int rowCount = 0;
            int colCount = 0;
            using (var workbook = new XLWorkbook())
            {
                using (var worksheet = workbook.Worksheets.Add(worksheetName))
                {
                    rowCount = 1;
                    foreach (var line in csvLines)
                    {
                        colCount = 1;
                        foreach (var col in line)
                        {
                            worksheet.Cell(rowCount, colCount).Value = TypeConverter.TryConvert(col);
                            colCount++;
                        }
                        rowCount++;
                    }

                }
                return workbook;
            }
        }
    }

    public class WrapperObject<T, Key, Value>
    {

        private ConcurrentBag<T> concurrentbag = new ConcurrentBag<T>();
        private ConcurrentDictionary<Key, Value> concurrentdictionary = new ConcurrentDictionary<Key, Value>();

        public List<T> GetList()
        {
            return concurrentbag.ToList<T>();
        }

        public void Add(T obj)
        {
            concurrentbag.Add(obj);
        }


        public Dictionary<Key, Value> GetDictionary()
        {
            //var newDictionary = concurrentdictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value, concurrentdictionary.Comparer);
            var myDictionary = concurrentdictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
            // or...
            // substitute your actual key and value types in place of TKey and TValue
            //var newDictionary = new Dictionary<K, V>(concurrentdictionary, concurrentdictionary.);
            return myDictionary;
        }

        public Value GetValue(Key key)
        {
            if (concurrentdictionary.Count > 0)
            {
                if (concurrentdictionary.ContainsKey(key))
                {
                    return concurrentdictionary[key];
                }
                else
                {
                    return default(Value);
                }

            }
            else
            {
                return default(Value);
            }
            
        }

        public void Add(Key key, Value value)
        {
            concurrentdictionary.AddOrUpdate(key, value, (keys, oldValue) => oldValue = value);
        }
    }

    public class Auditor
    {
        private static WrapperObject<string, string, string[]> analisis1 = new WrapperObject<string, string, string[]>();
        private static WrapperObject<string, string, string[]> analisis2 = new WrapperObject<string, string, string[]>();
        public static void SaveLog(string content = null, string worksheetname = null, int rowid = 0, IXLRangeRow row = null)
        {
            if (content != null && worksheetname == null && rowid == 0 && row == null)
            {
                analisis1.Add($"caso1{content}");
            }
            else if (content == null && worksheetname != null && rowid > 0 && row != null)
            {
                List<string> rowarray = new List<string>();
                row.CellsUsed().ForEach(m => rowarray.Add(TypeConverter.TryConvert(m.Value.ToString()).ToString()));
                analisis1.Add($"caso2@{rowid}@{worksheetname}@{Guid.NewGuid()}", rowarray.ToArray());
            }
            else if (content == "true" && worksheetname != null && rowid > 0 && row != null)
            {
                //string concat = "";
                ////row.CellsUsed().ForEach(m => concat = $"{concat},{TypeConverter.TryConvert(m.Value.ToString()).ToString()}");
                //concat = row.CellsUsed().Count() > 0 ? $"{TypeConverter.TryConvert(row.CellsUsed().ToList()[0].Value.ToString()).ToString()}" : "null";
                //analisis2.Add($"caso3@{rowid}@{worksheetname}@{concat}");
            }
        }

        //ConcurrentBag<string> log = new ConcurrentBag<string>();
        public static void SaveAnalysis(double elepsedtime, ValidatorResult<DTOValidacionArchivo> validator_result)
        {
            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //json_serializer.MaxJsonLength = 2147483647;
            //Task<string> output = Task<string>.Factory.StartNew(() => json_serializer.Serialize(validator_result.GetResult()));
            //Task.Factory.StartNew(() => IOUtilities.WriteLog(output.Result, "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_json_serializer.json", true));

            //Task.Factory.StartNew(() => SaveLog($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}\tTiempo actual {elepsedtime}."));
            //Task.Factory.StartNew(() => log.ForEach(m => IOUtilities.WriteLog(m, "PILOTO",$"{DateTime.Now.ToString("yyyy-MM-dd HH")}_linea.txt", false)));

            validator_result.GetResult().OrderBy(m => m.Columna).ForEach(m => IOUtilities.WriteLog(m.ToString(), "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_result_validation.txt", false));
            //Task.Factory.StartNew(() => validator_result.GetResult().OrderBy(m => m.Columna).ForEach(m => IOUtilities.WriteLog(m.ToString(), "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_result_validation.txt", false)));

            analisis1.GetList().ForEach(m => IOUtilities.WriteLog(m, "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_lista_de_errores.txt", false));
            //Task task1 = Task.Factory.StartNew(() => analisis1.GetList().ForEach(m => IOUtilities.WriteLog(m, "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_lista_de_errores.txt", false)));

            foreach (var keyvalue in analisis1.GetDictionary())
            {
                string concat = "";
                //keyvalue.Value.ForEach(m => concat = $"{concat},{m}");
                concat = keyvalue.Value.Count() > 0 ? $"@{keyvalue.Value[0]}" : "";
                IOUtilities.WriteLog($"{keyvalue.Key}{concat}", "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_lista_sin_errores.txt", false);
            }

            foreach (var keyvalue in analisis2.GetDictionary())
            {
                string concat = "";
                keyvalue.Value.ForEach(m => concat = $"{concat}@{m}");
                IOUtilities.WriteLog($"{keyvalue.Key}{concat}", "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_lineas_todos.txt", false);
            }

            foreach (var keyvalue in analisis2.GetList())
            {
                IOUtilities.WriteLog($"{keyvalue}", "PILOTO", $"{DateTime.Now.ToString("yyyy-MM-dd HH")}_lineas_todos.txt", false);
            }
            analisis1 = new WrapperObject<string, string, string[]>();
            analisis2 = new WrapperObject<string, string, string[]>();
            //Task.WaitAll(task1);
        }
    }

}
