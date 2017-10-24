using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SinchronizerV2
{
    /// <summary>
    /// Class for working with folders
    /// </summary>
    static class OperatinDirectory
    {
        /// <summary>
        /// method of creating a folder
        /// </summary>
        /// <param name="buf">folder creation path</param>
        static public void CrieteDirectory(string buf)
        {
            Directory.CreateDirectory(buf);
        }

        /// <summary>
        /// folder comparison method
        /// </summary>
        /// <see cref=" DelDir.Add()"/>
        /// <see cref="DeleteDir"()/>
        /// <param name="import">parameter for working with the class ImportSinchronizaer</param>
        /// <param name="export">parameter for working with the class ExportSinhronizaer</param>
        public static void TestDirectory(ImportSinchronizaer import, ExportSinhronizaer export)
        {
            List<string> DelDir = new List<string>();                                               //list for delete folders
            bool Add = false;
            foreach(var  dirEx in export.Dir)                                                       //open export list dirs
            {
                Add = false;
                string WayEx=dirEx.Remove((int)Nums.Zero, export.Root.Length);                      
                foreach(var dirIm in import.Dir)                                                    //open import list dirs
                {
                    string WayIm = dirIm.Remove((int)Nums.Zero, import.Root.Length);
                    if (WayEx == WayIm)                                                             //if way export==way import
                    {
                        Add = true;
                        break;
                    }
                }
                if (!Add)
                {
                    DelDir.Add(dirEx);                                                              //add in list delete folders
                }
            }
            DeleteDir(DelDir);                                                                     //call delete dir
        }

        /// <summary>
        /// method of deleting folders
        /// </summary>
        /// <param name="dirs">list of deleted folders</param>
        static public void DeleteDir(List<string> dirs)
        {
            for (var i = dirs.Count - (int)Nums.One; i >= (int)Nums.Zero; i--)
            {
                try
                {
                    Directory.Delete(dirs[i]);
                }
                catch(Exception e)
                {
                    Directory.Delete(dirs[i], true);
                }
                
            }
        }
    }
}
