using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Experiments
{
    internal class Experiments
    {
        static void Main(string[] args)
        {
            tail3();
        }

        /// <summary>
        /// Tail code from: https://www.codeproject.com/Articles/7568/Tail-NET
        /// </summary>
        static void tail1()
        {
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");

            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite)))
            {
                //start at the end of the file
                long lastMaxOffset = reader.BaseStream.Length;

                while (true)
                {
                    Thread.Sleep(50);

                    var currentLength = reader.BaseStream.Length;
                    if (currentLength < lastMaxOffset)
                    {
                        lastMaxOffset = 0;
                    }

                    //if the file size has not changed, idle
                    if (currentLength == lastMaxOffset)
                    {
                        continue;
                    }

                    //seek to the last max offset
                    reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                    //read out of the file until the EOF
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                        Console.WriteLine(line);

                    //update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;
                }
            }
        }

        /// <summary>
        /// Tests checking the length of the file, keeping the file only open for a short time,
        /// to avoid a file-sharing problem if the file is deleted and re-created by the logger.
        /// </summary><remarks>
        /// This sort-of works to observe the file length and to not prevent the logger from recreating
        /// the file. But it does not work all the time. If we are accessing the file while the logger
        /// tries to recreate it, the logger can fail.
        /// Note: I can see this sharing problem when using BareTail to observe the log file as well.
        ///       So I think it is just "a thing" and there may not be much to do about it.
        /// </remarks>
        static void tail2()
        {
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");

            long length = 0;
            while (true)
            {
                Thread.Sleep(100);
                long newLength;
                using (var f = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite))
                {
                    newLength = f.Length;
                }
                if (newLength != length)
                {
                    length = newLength;
                    Console.WriteLine(length);
                }
            }
        }

        /// <summary>
        /// Tails a file, reading characters from the file instead of lines (as is done by tail1).
        /// </summary>
        static void tail3()
        {
            var path = Path.Combine(m_solutionFolder, "LogFiles/LogFile1.log");
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite)))
            {
                // We find the current end-of-file position...
                long previousFilePosition = reader.BaseStream.Length;
                while (true)
                {
                    Thread.Sleep(100);

                    // We check to see if there is new data in the file...
                    var newFilePosition = reader.BaseStream.Length;
                    if (newFilePosition < previousFilePosition)
                    {
                        // The file has reduced in size. Likely it was deleted and re-created...
                        previousFilePosition = 0;
                    }
                    if (newFilePosition == previousFilePosition)
                    {
                        // There is no new data logged to the file...
                        continue;
                    }

                    // We read new data from the file between the previous and new file positions...
                    var numChars = (int)(newFilePosition - previousFilePosition);
                    var buffer = new char[numChars];
                    reader.BaseStream.Seek(previousFilePosition, SeekOrigin.Begin);
                    reader.ReadBlock(buffer, 0, numChars);
                    Console.Write(buffer);

                    // We update the position we just read...
                    previousFilePosition = reader.BaseStream.Position;
                }
            }

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
