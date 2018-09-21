using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAC.Library.Utilities
{
    [Serializable]
    public class IOUtilities
    {
        private static void SaveException(Exception ex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string wrapper = string.Format("{0}\t{3}\t{1}\t{2}", GetLocalTime(), ex.Source, ex.ToString().Replace(Environment.NewLine, ""), Configuration.GetClassName<IOUtilities>());
                sb.Append(wrapper);
                sb.AppendLine();
                string defaultPath = GetDefaultPath(Configuration.GetClassName<IOUtilities>(), FileAttributes.Hidden);
                File.AppendAllText(defaultPath + Configuration.GetClassName<IOUtilities>(), sb.ToString());
                sb.Clear();
            }
            catch (Exception)
            {

            }
        }

        public static string GetDefaultPath(string newFolder, FileAttributes fileAttributes)
        {
            try
            {
                string hosting = System.Web.Hosting.HostingEnvironment.MapPath(Configuration.GetValueConf(Constants.TemporalServerFolder));
                string special = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string folder = special.Length > 0 ? special : hosting;
                string path = string.Format(@"{0}\{2}\{1}", folder, newFolder, Configuration.GetValueConf(Constants.DebuggerFolder));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    DirectoryInfo tem = new DirectoryInfo(path) { Attributes = fileAttributes };
                }
                return path;
            }
            catch (Exception ex)
            {
                SaveException(ex);
                throw ex;
            }
        }

        public static string GetLocalTime(string format = "yyyy-MM-dd HH:mm:ss")
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(Configuration.GetValueConf(Constants.TimeZoneInfo))).ToString(format);
        }

        /// <summary>
        /// Custom console log.
        /// </summary>
        /// <param name="folder">folder path name</param>
        /// <param name="filename">filename</param>
        /// <param name="content">custom content</param>
        /// <param name="deleteprev">true for delete previous file or false for append lines</param>
        public static void WriteLog(string content, string folder = @"IOUtilities", string filename = @"\exception.txt", bool deleteprev = false)
        {
            try
            {
                string path = string.Format(@"{0}\{1}", GetDefaultPath(folder, FileAttributes.Normal), filename);
                if (File.Exists(path) == true && deleteprev == true)
                {
                    File.Delete(path);
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(content);
                sb.AppendLine();
                File.AppendAllText(path, sb.ToString());
            }
            catch (Exception ex)
            {
                SaveException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Custom Console for save a log with exception and events
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="servicename"></param>
        /// 
        public static void WriteExceptionLog(Exception ex, string servicename)
        {
            try
            {
                string path = string.Format(@"{0}\{1}", GetDefaultPath(Configuration.GetClassName<IOUtilities>(), FileAttributes.Hidden), Configuration.GetValueConf(Constants.ExceptionFile));
                string content = string.Format("{0}\t{1}\t{2}\t{3}", GetLocalTime(), servicename, ex.Source, ex.ToString().Replace(Environment.NewLine, ""));
                StringBuilder sb = new StringBuilder();
                sb.Append(content);
                sb.AppendLine();
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
            }
            catch (Exception)
            {
                SaveException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Read a file as text
        /// </summary>
        /// <param name="fullpathname">full pathname</param>
        /// <returns>a string with all text</returns>
        public static string ReadAllText(string fullpathname)
        {
            try
            {
                string stg = File.Exists(fullpathname) ? File.ReadAllText(fullpathname) : null;
                return stg;
            }
            catch (Exception ex)
            {
                SaveException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Read all lines from file as text
        /// </summary>
        /// <param name="fullpathname">full pathname</param>
        /// <returns>a string array</returns>
        public static string[] ReadAllLines(string fullpathname)
        {
            try
            {
                string[] stg = File.Exists(fullpathname) ? File.ReadAllLines(fullpathname) : null;
                return stg;
            }
            catch (Exception ex)
            {
                SaveException(ex);
                throw ex;
            }
        }
    }
}
