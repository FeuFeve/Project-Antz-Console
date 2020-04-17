using System;
using System.IO;
using Newtonsoft.Json;

namespace Project_Antz_Console
{
    internal class JsonReader
    {
        internal static dynamic ReadFile(string path)
        {
            // Get the content of the json file
            string json = File.ReadAllText(path);
            
            dynamic array = JsonConvert.DeserializeObject(json);
            return array;
        }
    }
}