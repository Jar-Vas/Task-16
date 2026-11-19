using System;
using System.Collections.Generic;
using System.Text;

namespace Task_16
{
    internal class Logger
    {
        public readonly string file;
        public Logger(string file) { this.file = file; }
        public void Info(string message) { Console.WriteLine("Info: " + message); }
        public void Error(string message, Exception? e)
        {
            Console.WriteLine("Error: " + message);
            if (e != null) {  Console.WriteLine(e.ToString()); }
        }
        public void Write(string massage)
        {
            File.WriteAllText(file, massage + "\n");
        }
    }
}
