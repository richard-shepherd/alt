using System;
using System.IO;

namespace LogFileGenerator
{
    /// <summary>
    /// Creates sample log files.
    /// </summary>
    internal class Program
    {
        // Log generation paramaters...
        static string PATH = "../../../LogFiles/LogFile1.log";
        static int NUM_LINES = 100000;
        static double MAX_SECONDS_BETWEEN_LINES = 1.0;
        static DateTime START_TIMESTAMP = new DateTime(2024, 1, 10);
        static double ERROR_PROBABILITY = 0.01;
        static double WARN_PROBABILITY = 0.03;

        /// <summary>
        /// Main.
        /// </summary>
        static void Main(string[] args)
        {
            // We make sure the folder exists...
            Directory.CreateDirectory(Path.GetDirectoryName(PATH));

            // We write log lines to the file with the params specified above...
            var timestamp = START_TIMESTAMP;
            using (var filestream = File.OpenWrite(PATH))
            using (var streamWriter = new StreamWriter(filestream))
            {
                for (var i = 0; i < NUM_LINES; i++)
                {
                    streamWriter.WriteLine($"{timestamp:yyyy-MM-dd:HH:mm:ss.fff}: {logLevel()}: Line {i}");
                    timestamp += TimeSpan.FromSeconds(m_rnd.NextDouble() * MAX_SECONDS_BETWEEN_LINES);
                }
            }
        }

        /// <summary>
        /// Returns a random log level based on the specified probabilities.
        /// </summary>
        static string logLevel()
        {
            var rnd = m_rnd.NextDouble();
            if(rnd < ERROR_PROBABILITY)
            {
                return "ERROR";
            }
            if(rnd < ERROR_PROBABILITY + WARN_PROBABILITY)
            {
                return "WARN";
            }
            return "INFO";
        }

        // Used for random time intervals bewteen lines, random log levels etc...
        static Random m_rnd = new Random();
    }
}
