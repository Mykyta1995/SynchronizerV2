using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace SinchronizerV2
{
    /// <summary>
    /// class for executing operations with files
    /// </summary>
    static class ComparisonFile
    {
        /// <summary>
        /// file comparison method
        /// </summary>
        /// <see cref="TestFileExport()"/>
        /// <see cref="CopyFile"/>
        /// <see cref="export.AddNewFile()"/>
        /// <param name="import">parameter for working with the class ImportSinchronizaer</param>
        /// <param name="export">parameter for working with the class ExportSinhronizaer</param>
        static public void TestFile(ImportSinchronizaer import, ExportSinhronizaer export)
        {
            foreach (var fileIm in import.File)                                                                      //open file int import for comparison
            {
                FileStream fileImpor = new FileStream(fileIm, FileMode.Open, FileAccess.Read);                       //open file
                string Way = String.Format("{0}\\{1}", export.Root, fileIm.Remove((int)Nums.Zero, import.Root.Length));  //create way for comparison
                bool flag = true;                                                                                     //flag for copy file or not
                if (export.Files.Count > (int)Nums.Zero)                    
                {
                    flag = TestFileExport(export, fileImpor, fileIm, Way);                                            //call method export object
                }
                if (flag)
                {
                    CopyFile(fileImpor, Way);                                                                        //call method for copy
                    export.AddNewFile(Way);                                                                          //call method for add new dir
                }
            }
        }

        /// <summary>
        /// file comparison method
        /// </summary>
        /// <see cref="MD5Hash"/>
        /// <see cref=" export.Remove"/>
        /// <see cref="MoveFile"/>
        /// <see cref=" export.AddMoveFile"/>
        /// <param name="export">parameter for working with the class ExportSinhronizaer</param>
        /// <param name="fileImpor">file for comparison in import</param>
        /// <param name="fileIm">the path to the file</param>
        /// <param name="Way">file transfer path</param>
        /// <returns>the file was moved or not</returns>
        private static bool TestFileExport(ExportSinhronizaer export, FileStream fileImpor, string fileIm, string Way)
        {
            bool flag = true;
            foreach (var fileEx in export.Files)
            {
                if (!File.Exists(fileEx))                                                  
                {
                    continue;
                }
                FileStream fileExport = new FileStream(fileEx, FileMode.Open, FileAccess.Read);               //open export file
                if (fileImpor.Length == fileExport.Length)                                                    
                {
                    flag = MD5Hash(fileImpor, fileExport);                                                   //method for comparison
                    fileExport.Close();                                                                      //close export file
                    if (!flag)
                    {
                        export.Remove(fileEx);                                                              //method for remove export file
                        MoveFile(fileEx, Way);
                        export.AddMoveFile(Way, fileEx);
                        break;
                    }
                }
                else fileExport.Close();                                                                   //close export file
            }
            return flag;
        }


        /// <summary>
        /// file comparison method
        /// </summary>
        /// <param name="fileImpor">file import</param>
        /// <param name="fileExport">file import</param>
        /// <returns>same or not</returns>
        private static bool MD5Hash(FileStream fileImpor, FileStream fileExport)
        {
            MD5 md = MD5.Create();
            var HashImport = md.ComputeHash(fileImpor);
            string HashIm = BitConverter.ToString(HashImport).Replace("-", String.Empty);
            var HashExport = md.ComputeHash(fileExport);
            string HashEx = Encoding.Unicode.GetString(HashExport);
            return HashIm.CompareTo(HashEx) == (int)Nums.Zero;
        }

        /// <summary>
        /// method of displacement files
        /// </summary>
        /// <param name="fileEx">file import</param>
        /// <param name="Way">file transfer path</param>
        static public void MoveFile(string fileEx, string Way)
        {
            Console.WriteLine(fileEx);
            try
            {
                if (File.Exists(fileEx))
                {
                    File.Move(fileEx, Way);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// copy method
        /// </summary>
        /// <param name="importFile">import file</param>
        /// <param name="Way">file transfer path</param>
        static private void CopyFile(FileStream importFile, string Way)
        {
            Console.WriteLine(importFile.Name);
            FileStream exportFile = new FileStream(Way, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[(int)Nums.Cluster];
            while (true)
            {
                var iRead = importFile.Read(buf, (int)Nums.Zero, buf.Length);
                if (iRead == (int)Nums.Zero) break;
                exportFile.Write(buf, (int)Nums.Zero, iRead);
            }
            exportFile.Close();
        }

        /// <summary>
        /// method of deleting folders
        /// </summary>
        /// <param name="files">list of folders</param>
        static public void DeleteFile(List<string> files)
        {
            foreach (var file in files)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch (Exception e)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }
            }
        }
    }
}
