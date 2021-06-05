using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenbrugerApp
{
    public class Logger
    {
        private static string path = Directory.GetCurrentDirectory();
        public static void SaveMessage(string message)
        {
            using (StreamWriter writer = File.AppendText(path + "/Logger/Log.txt"))
            {
                Log(message, writer);
            }
        }

        private static void Log(string message, TextWriter writer)
        {
            writer.WriteLine($"{message} den {DateTime.Now}\n\n");
        }
    }
}
