using System;
using System.IO;
using System.Text;

namespace BahcesehirUniversitesiWissenAkademie.Loggers
{
    public static class FileLogger
    {
        private static string _path = @"D:\Log.txt";
        public static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(_path, true, Encoding.UTF8))
            {
                writer.WriteLine(DateTime.Now + " " + message);
                writer.Close();
            }
        }
    }
}
