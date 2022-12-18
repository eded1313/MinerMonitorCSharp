using Miner.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerDaemon.Helper
{
    public interface ILogger
    {
        string FilePath { get; }
        void Info(string message);
        void Error(string message);
        void Warning(string message);
    }
    public class Logger : ILogger
    {
        public string FilePath => Setting.GetAbsolutePath();

        public Logger()
        {
        }

        public async Task WriteFileAsync(string file, string content)
        {
            Console.WriteLine("Async Write File has started");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(file)))
            {
                await outputFile.WriteAsync(content);
            }
            Console.WriteLine("Async Write File has completed");
        }

        public void Info(string message)
        {
            Console.WriteLine($"Info: {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }
    }
}
