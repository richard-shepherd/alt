using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Experiments
{
    internal class Experiments
    {
        static void Main(string[] args)
        {
            countLines1();
            countLines4();
        }

        /// <summary>
        /// Counts lines using File.ReadLines().
        /// </summary><remarks>
        /// ReadLines returns an enumerable - so the lines are not all held in memory at the same time...
        /// </remarks>
        static void countLines1()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");
            var count = File.ReadLines(path).Count();
            Console.WriteLine($"1 - Found {count} lines in {stopwatch.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// Counts lines using a StreamReader while mapping line-number -> position
        /// for every 1000th line.
        /// </summary>
        static void countLines2()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");
            var lineMap = new Dictionary<int, long>();
            var count = 0;
            using (var streamReader = new StreamReader(path))
            {
                for (; ; )
                {
                    var position = streamReader.BaseStream.Position;
                    var line = streamReader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (count % 1000 == 0)
                    {
                        lineMap[count] = position;
                    }
                    count++;
                }
            }
            Console.WriteLine($"2 - Found {count} lines in {stopwatch.ElapsedMilliseconds}ms. Mapped-lines={lineMap.Count}");
        }

        /// <summary>
        /// Counts lines using ReadAllLines(). This holds all the data in memory.
        /// </summary>
        static void countLines3() 
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");
            var lines = File.ReadAllLines(path);
            Console.WriteLine($"3 - Found {lines.Length} lines in {stopwatch.ElapsedMilliseconds}ms.");
        }

        /// <summary>
        /// Estimates the number of lines in the file.
        /// </summary>
        static void countLines4()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");

            // We estimate the number of lines from the position after 
            // reading the first n lines...
            var linesToRead = 10000;
            var count = 0;
            long position = 0;
            long fileSize = 0;
            using (var streamReader = new StreamReader(path))
            {
                fileSize = streamReader.BaseStream.Length;
                for (; ; )
                {
                    position = streamReader.BaseStream.Position;
                    var line = streamReader.ReadLine();
                    count++;
                    if (count == linesToRead)
                    {
                        break;
                    }
                }
            }
            var estimatedLines = (int)((double)linesToRead / position * fileSize);
            Console.WriteLine($"4 - Estimated {estimatedLines} lines in {stopwatch.ElapsedMilliseconds}ms.");
        }

        // Path to the solution folder from the build folder...
        static string m_solutionFolder = "../../../..";
    }
}
