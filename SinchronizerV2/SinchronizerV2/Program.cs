using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SinchronizerV2
{
    enum Nums : uint {Zero = 0, One, Seven = 7, Ten = 10,Fifteen=15,Twenty=20,Cluster=4096};
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sin = Sinhronizer.Create();
                sin.CreateDir();
                sin.Comparisone();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Finish");
                Console.ReadKey();
            }
        }
    }
}
