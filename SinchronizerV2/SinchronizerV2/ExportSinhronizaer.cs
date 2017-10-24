using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SinchronizerV2
{
    /// <summary>
    /// folder export class
    /// </summary>
    [Serializable]
    class ExportSinhronizaer:SinchronizaerBase
    {
        /// <summary>
        /// list files
        /// </summary>
        private List<string> files = new List<string>();
        /// <summary>
        /// list dirs
        /// </summary>
        private List<string> dir = new List<string>();
        /// <summary>
        /// list new dir in root directory
        /// </summary>
        private List<string> newDir = new List<string>();
        /// <summary>
        /// list new file in root directory
        /// </summary>
        private List<string> newFile = new List<string>();
        /// <summary>
        /// dictionary for move file
        /// </summary>
        private Dictionary<string, string> moveFile = new Dictionary<string, string>();

        /// <summary>
        /// Method for adding a new folder
        /// </summary>
        /// <param name="way">The path to the new folder</param>
        public void AddNewDir(string way)
        {
            this.newDir.Add(way);
        }

        /// <summary>
        /// Method for adding a new file
        /// </summary>
        /// <param name="way">The path to the new file</param>
        public void AddNewFile(string way)
        {
            this.newFile.Add(way);
        }

        /// <summary>
        /// Method for adding a move file
        /// </summary>
        /// <param name="key">new way</param>
        /// <param name="value">old way</param>
        public void AddMoveFile(string key, string value)
        {
            this.moveFile.Add(key, value);
        }

        /// <summary>
        /// <value>property for checking is ended recursive bypass</value>
        /// </summary>
        public bool Complete { set; get; }
        
        /// <summary>
        /// <value>the property of stopping a recursive crawl of a folder</value>
        /// </summary>
        public string StopWaySearch { set; get; }

        /// <summary>
        /// method of starting recursive traversal
        /// </summary>
        /// <param name="s">root of the bypass folder</param>
        /// <param name="obj">serialization object</param>
        public void Open(Sinhronizer obj)
        {
            if(!Directory.Exists(this.Root))
            {
                OperatinDirectory.CrieteDirectory(this.Root);
            }
            base.WalkRir(new DirectoryInfo(this.Root), files, dir, obj, (int)Nums.Zero, dir.Count == (int)Nums.Zero);
        }

        /// <summary>
        /// <value>folder-property</value>
        /// </summary>
        public List<string> Dir { get { return this.dir; } }

        /// <summary>
        /// <value>root search root property</value>
        /// </summary>
        public string Root { set; get; }

        /// <summary>
        /// <value>files-property</value>
        /// </summary>
        public List<string> Files { get { return this.files; } }

        /// <summary>
        /// method of deleting a file
        /// </summary>
        /// <param name="key">deletion key</param>
        public void Remove(string key)
        {
            this.files.Remove(key);
        }

        /// <summary>
        /// method of cleaning values
        /// </summary>
        public void Reset()
        {
            this.dir.Clear();
            this.Dir.Clear();
            this.files.Clear();
            this.Files.Clear();
            this.Root = null;
            this.StopWaySearch = null;
            this.moveFile.Clear();
            this.newDir.Clear();
            this.newFile.Clear();
        }

        /// <summary>
        /// method for cleaning a folder in case of an error
        /// </summary>
         public void ClearError()
        {
            ComparisonFile.DeleteFile(this.newFile);
            OperatinDirectory.DeleteDir(this.newDir);
            foreach(var key in this.moveFile.Keys)
            {
                ComparisonFile.MoveFile(key, this.moveFile[key]);
            }
        }
    }
}
