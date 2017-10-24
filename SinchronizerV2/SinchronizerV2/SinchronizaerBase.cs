using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;

namespace SinchronizerV2
{   
    /// <summary>
    /// This is the base class ExportSinhronizaer and ImportSinchronizaer
    /// </summary>
    [Serializable]
    public abstract class SinchronizaerBase
    {
        /// <summary>
        /// The string collection
        /// </summary>
        static StringCollection log = new StringCollection();
        /// <summary>
        /// menu display field
        /// </summary>
        int size = (int)Nums.Zero;
        /// <summary>
        /// menu display field
        /// </summary>
        int cursor = (int)Nums.Seven;

        /// <summary>
        /// recursive traversal method
        /// </summary>
        /// <param name="root">root directory</param>
        /// <param name="file">write files</param>
        /// <param name="dir">write directoru</param>
        /// <param name="obj">serialization object</param>
        /// <param name="count">file counter for serialization</param>
        /// <param name="common">flag to continue traversal</param>
        public void WalkRir(DirectoryInfo root, List<string> file, List<string> dir, Sinhronizer obj, int count = (int)Nums.Zero, bool common = false)
        {
            DirectoryInfo[] di = null;                                                           //array for directory
            FileInfo[] files = null;                                                             //array for files
            if (!common && dir[dir.Count - (int)Nums.One] == root.FullName) common = true;       
            try
            {
                files = root.GetFiles();                                                         //add files in array files
            }
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            if (files != null)                                                               //if no files
            {
                if (common)
                {
                    foreach (var i in files)
                    {
                        if (!file.Contains(i.FullName))
                        {
                            count++;
                            file.Add(i.FullName);
                            this.size++;
                            if (size % (int)Nums.Ten == (int)Nums.Zero)
                            {
                                Waiting();
                            }
                            if (count % (int)Nums.Ten == (int)Nums.Zero)
                            {
                                MySerialization.Serialization(obj, obj.WaySaveFile);                          //serialization 10 files
                            }
                            else if (count % (int)Nums.Fifteen == (int)Nums.Zero)
                            {
                                MySerialization.Serialization(obj, obj.WaSaveFile2);                         //serialization 15 files
                            }
                        }
                    }
                }
                di = root.GetDirectories();                                                                  //add all dir root dir
                foreach (var j in di)
                {
                    if (dir == null || !dir.Contains(j.FullName)) dir.Add(j.FullName);
                    WalkRir(j, file, dir, obj, count, common);                                              //add dir in array dirs   
                }
            }
        }

        /// <summary>
        /// menu output method
        /// </summary>
        private void Waiting()
        {
            if (cursor > (int)Nums.Twenty) cursor = (int)Nums.Seven;                                     //if cursor>20=>kursor=7
            this.cursor++;
            Console.Clear();                                                                             //clear console
            Console.Write("Waiting:");
            Console.CursorLeft = cursor;                                                                 //left cursor
            Console.Write('#');
        }

    }
}
