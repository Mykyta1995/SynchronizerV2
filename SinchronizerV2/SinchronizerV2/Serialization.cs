using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;

namespace SinchronizerV2
{
    /// <summary>
    /// serialization class
    /// </summary>
    class MySerialization
    {
        /// <summary>
        /// method Serialization
        /// </summary>
        /// <param name="obj">serialization object</param>
        /// <param name="WaySaveFile">file save path</param>
        public static void Serialization(Sinhronizer obj,string WaySaveFile)
        {
            FileStream fs = new FileStream(WaySaveFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, obj);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                fs.Close();
            }
        }


        /// <summary>
        /// method Deserialize
        /// </summary>
        /// <param name="sin">Deserialize object</param>
        public static void Deserialize(ref Sinhronizer sin)
        {
            FileStream fs = new FileStream(sin.WaySaveFile, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                sin = (Sinhronizer)formatter.Deserialize(fs);
            }
            catch(SerializationException e)
            {
                fs.Close();
                fs = new FileStream(sin.WaSaveFile2, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                sin = (Sinhronizer)formatter.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }
            
        }
    }
}
