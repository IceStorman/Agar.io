using System;
using System.IO;
using Newtonsoft.Json;

namespace Agario
{
    internal static class FileManager
    {
        public static GameProperties gameProperties = new GameProperties();

        public static void CreateFile()
        {
            if (!IsFileExist())
            {
                string properties = JsonConvert.SerializeObject(gameProperties, Formatting.Indented);
                Console.WriteLine($"Created data: {properties}");

                StreamWriter sw = new StreamWriter("gameProperties.ini");
                sw.WriteLine(properties);
                sw.Close();
            }
        }

        public static void LoadFile()
        {
            StreamReader sr = new StreamReader("gameProperties.ini");
            string properties = sr.ReadToEnd();
            Console.WriteLine($"Loaded data: {properties}");
            gameProperties = JsonConvert.DeserializeObject<GameProperties>(properties);
            sr.Close();
        }

        private static bool IsFileExist()
        {
            return File.Exists(@"gameProperties.ini") ? true : false;
        }        

        public static string SetStringValue(string value, string defaultValue) =>
            string.IsNullOrEmpty(value) ? defaultValue : value;

        public static uint? SetUintValue(uint? value, uint defaultValue) =>
            value == null ? defaultValue : value;
    }
}
