using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SinchronizerV2
{
    /// <summary>
    /// generic synchronization class
    /// </summary>
    [Serializable]
    public class Sinhronizer
    {
        /// <summary>
        /// class fields export
        /// </summary>
        private ExportSinhronizaer export = new ExportSinhronizaer();

        /// <summary>
        /// class fields import
        /// </summary>
        private ImportSinchronizaer import = new ImportSinchronizaer();

        /// <summary>
        /// filed way Serialize file first
        /// </summary>
        public readonly string WaySaveFile = @"Sinhronizer1.dat";

        /// <summary>
        /// filed way Serialize file second
        /// </summary>
        public readonly string WaSaveFile2 = @"Sinhronizer2.dat";


        /// <summary>
        /// method for create object and read Serialize
        /// </summary>
        /// <returns>object type Sinhronizer</returns>
        public static Sinhronizer Create()
        {
            Sinhronizer sin = new Sinhronizer();
            if (File.Exists(sin.WaySaveFile))
            {
                MySerialization.Deserialize(ref sin);
                Console.WriteLine("1)Continue synchronization");
                Console.WriteLine("Import:{0}", sin.import.Root);
                Console.WriteLine("Export:{0}",sin.export.Root);
                Console.WriteLine("2)Resync start");
                int choise = sin.TestChoise();
                if (choise == 2)
                {
                    NewSinhronizer(sin);
                }
            }
            else NewSinhronizer(sin);
            MySerialization.Serialization(sin, sin.WaySaveFile);
            return sin;
        }

        /// <summary>
        /// method for entering new roots
        /// </summary>
        /// <param name="sin">object Sinhronizer</param>
        private static void NewSinhronizer(Sinhronizer sin)
        {
            sin.Reset();
            Console.Write("Press import directory:");
            string importS = Console.ReadLine();
            Console.Write("Press export directory:");
            string exportS = Console.ReadLine();
            if (importS.CompareTo(exportS) == (int)Nums.Zero) return;
            sin.import.Root = importS;
            sin.export.Root = exportS;
            sin.import.Open(sin);
            sin.import.Complete = true;
            sin.export.Open(sin);
            sin.export.Complete = true;
        }

        /// <summary>
        /// method for reset object
        /// </summary>
        public void Reset()
        {
            if (File.Exists(WaySaveFile)) File.Delete(WaySaveFile);
            if (File.Exists(WaSaveFile2)) File.Delete(WaSaveFile2);
            this.export.Reset();
            this.import.Reset();
        }

        /// <summary>
        /// method for create folders
        /// </summary>
        public void CreateDir()
        {
            foreach (var dir in this.import.Dir)
            {
                var buf = string.Format("{0}\\{1}", this.export.Root, dir.Remove((int)Nums.Zero, this.import.Root.Length));
                if (!Directory.Exists(buf))
                {
                    this.export.AddNewDir(buf);
                    OperatinDirectory.CrieteDirectory(buf);
                }
            }
        }

        /// <summary>
        /// method of starting all processes
        /// </summary>
        public void Comparisone()
        {
            try
            {
                if (!Directory.Exists(this.import.Root))
                {
                    Console.WriteLine("Error open directory");
                    return;
                }
                ComparisonFile.TestFile(this.import, this.export);
                ComparisonFile.DeleteFile(this.export.Files);
                OperatinDirectory.TestDirectory(this.import, this.export);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                Console.ReadKey(true);
                this.export.ClearError();
            }
            finally
            {
                this.Reset();
            }
        }


        /// <summary>
        /// data input validation method
        /// </summary>
        /// <returns>choise</returns>
        private int TestChoise()
        {
            string s = null;
            int choise = (int)Nums.Zero;
            while (true)
            {
                Console.Write("Your choice:");
                s = Console.ReadLine();
                if (int.TryParse(s, out choise))
                {
                    if (choise > (int)Nums.Zero && choise < 3) break;
                }
            }
            return choise;
        }
    }
}
