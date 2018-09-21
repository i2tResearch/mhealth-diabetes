using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;

namespace CAC.Library.Utilities
{
    /// <summary>
    /// Allows compress and decompress files in format *.gz
    /// https://msdn.microsoft.com/en-us/library/ms404280(v=vs.100).aspx
    /// </summary>
    public class ZipFile
    {
        public static byte[] CompressGZipStream(byte[] array)
        {
            try
            {
                byte[] response = new byte[array.Length];
                using (MemoryStream msOut = new MemoryStream())
                {
                    using (MemoryStream msIn = new MemoryStream(array))
                    {
                        using (GZipStream decompress = new GZipStream(msIn, CompressionMode.Compress))
                        {
                            decompress.CopyTo(msOut);
                            decompress.Close();
                        }
                        msIn.Close();
                    }
                    response = msOut.ToArray();
                    msOut.Close();
                }
                return response;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, "ZipFile");
                return null;
            }
        }

        public static string CompressGZipStream(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);
                            IOUtilities.WriteLog(string.Format("Compressed {0} from {1} to {2} bytes.", fi.Name, fi.Length.ToString(), outFile.Length.ToString()));
                            return outFile.Name;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public static byte[] DecompressGZipStream(byte[] array)
        {
            try
            {
                byte[] response = new byte[array.Length];
                using (MemoryStream msOut = new MemoryStream())
                {
                    using (MemoryStream msIn = new MemoryStream(array))
                    {
                        using (GZipStream decompress = new GZipStream(msIn, CompressionMode.Decompress))
                        {
                            decompress.CopyTo(msOut);
                            decompress.Close();
                        }
                        msIn.Close();
                    }
                    response = msOut.ToArray();
                    msOut.Close();
                }
                return response;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, "ZipFile");
                return null;
            }
        }

        public static string DecompressGZipStream(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);
                        return outFile.Name;
                    }
                }
            }
        }

        public static byte[] DecompressZipStream(byte[] array)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration.GetValueConf(Constants.TemporalFolder));
                string zipPath = $@"{path}\temp.zip";
                string pathextract = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration.GetValueConf(Constants.TemporalFolder), "output"); 
                Directory.CreateDirectory(path);

                File.WriteAllBytes(zipPath, array);
                
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, pathextract);
                
                byte[] response = new byte[array.Length];
                var files= Directory.EnumerateFiles(pathextract).ToList();
                if (files.Count() > 0)
                {
                    var info = files.ElementAt(0);
                    response = File.ReadAllBytes(info);
                    File.Delete(info);
                }
                Directory.Delete(path, true);
                return response;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, "ZipFile");
                return null;
            }
        }

        public static string DecompressZipStream(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);
                        return outFile.Name;
                    }
                }
            }
        }
    }
}