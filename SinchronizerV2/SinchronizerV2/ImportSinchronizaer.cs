using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SinchronizerV2
{
    /// <summary>
    /// import export class
    /// </summary>
    [Serializable]
    class ImportSinchronizaer : SinchronizaerBase
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
        /// <value>property for checking is ended recursive bypass</value>
        /// </summary>
        public bool Complete { set; get; }

        /// <summary>
        /// method of starting recursive traversal
        /// </summary>
        /// <param name="s">root of the bypass folder</param>
        /// <param name="obj">serialization object</param>
        public void Open(Sinhronizer obj)
        {
            base.WalkRir(new DirectoryInfo(this.Root), files,dir,obj,0,dir.Count==0);
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
        public List<string> File { get { return this.files; } }

        /// <summary>
        /// method of cleaning values
        /// </summary>
        public void Reset()
        {
            this.dir.Clear();
            this.Dir.Clear();
            this.File.Clear();
            this.files.Clear();
            this.Root = null;
        }

    }
}
