using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes
{
    public static class JsonFileUtils
    {
        /// <summary>
        /// Writes an indented json file
        /// </summary>
        /// <param name="obj"> String that will be converted to json</param>
        /// <param name="fileName"> Name or full path of the output file</param>
        public static void PrettyWrite(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
