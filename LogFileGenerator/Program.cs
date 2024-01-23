using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogFileGenerator
{
    /// <summary>
    /// Creates sample log files.
    /// </summary>
    internal class Program
    {
        // Log generation paramaters...
        static string PATH = "../../../LogFiles/LogFile1.log";
        static int NUM_LINES = 25000000;
        static double MAX_SECONDS_BETWEEN_LINES = 0.01;
        static DateTime START_TIMESTAMP = new DateTime(2024, 1, 10);
        static double ERROR_PROBABILITY = 0.01;
        static double WARN_PROBABILITY = 0.03;
        static double CAPITAL_NOUN_PROBABILITY = 0.2;

        /// <summary>
        /// Main.
        /// </summary>
        static void Main(string[] args)
        {
            // We make sure the folder exists...
            Directory.CreateDirectory(Path.GetDirectoryName(PATH));
            File.Create(PATH).Close();

            // We write log lines to the file with the params specified above...
            var timestamp = START_TIMESTAMP;
            using (var streamWriter = new StreamWriter(PATH))
            {
                for (var i = 0; i < NUM_LINES; i++)
                {
                    streamWriter.WriteLine(createLogLine(timestamp));
                    timestamp += TimeSpan.FromSeconds(m_rnd.NextDouble() * MAX_SECONDS_BETWEEN_LINES);
                    if(i%1000 == 0)
                    {
                        Console.Write(".");
                    }
                }
            }
        }

        /// <summary>
        /// Creates a log line with random information for the timestamp provided.
        /// </summary>
        static string createLogLine(DateTime timestamp)
        {
            var line = new StringBuilder();

            // Timestamp...
            line.Append($"{timestamp:yyyy-MM-dd HH:mm:ss.fff} ");

            // Log level...
            line.Append($"{logLevel()}: ");

            // Text...
            line.Append($"{phrase(true)} by {phrase(false)} from {shortPhrase()} with {shortPhrase()}");

            return line.ToString();
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

        /// <summary>
        /// Returns a phrase verb-adjective-noun.
        /// </summary>
        static string phrase(bool initialCapital)
        {
            var phrase = $"{verb()} {adjective()} {noun()}";
            if (initialCapital)
            {
                phrase = phrase[0].ToString().ToUpper() + phrase.Substring(1);
            }
            return phrase;

        }

        /// <summary>
        /// Returns a phrase adjective-noun.
        /// </summary>
        static string shortPhrase()
        {
            return $"{adjective()} {noun()}";
        }

        /// <summary>
        /// Returns a random verb.
        /// </summary>
        static string verb()
        {
            return m_verbs[m_rnd.Next(m_verbs.Count)];
        }

        /// <summary>
        /// Returns a random noun.
        /// </summary>
        static string noun()
        {
            var noun = m_nouns[m_rnd.Next(m_nouns.Count)];
            if(m_rnd.NextDouble() < CAPITAL_NOUN_PROBABILITY)
            {
                noun = noun.ToUpper();
            }
            return noun;
        }

        /// <summary>
        /// Returns a random adjective.
        /// </summary>
        static string adjective()
        {
            return m_adjectives[m_rnd.Next(m_adjectives.Count)];
        }

        // Used for random time intervals bewteen lines, random log levels etc...
        static Random m_rnd = new Random();

        // Lists of strings which can be combined to make log lines...
        static List<string> m_verbs = new List<string>
        {
            "processing",
            "retrieving",
            "calculating",
            "cross-referencing",
            "inverting",
            "storing",
            "upscaling"
        };
        static List<string> m_nouns = new List<string>
        {
            "data",
            "payments",
            "packets",
            "pages",
            "information",
            "penguins",
            "database"
        };
        static List<string> m_adjectives = new List<string>
        {
            "customer",
            "rapid",
            "inventory",
            "up-to-date",
            "latest",
            "historic",
            "failed",
            "queried",
            "discarded",
            "obsolete",
            "deprecated",
            "meta"
        };


    }
}
