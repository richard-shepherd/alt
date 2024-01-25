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
            // Counting lines 1...
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var path = "../../../../LogFiles/LogFile1.log";
                var count = File.ReadLines(path).Count();
                Console.WriteLine($"1 - Found {count} lines in {stopwatch.ElapsedMilliseconds}ms");
            }

            //// Counting lines 2...
            //{
            //    var stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    var path = "../../../../LogFiles/LogFile1.log";
            //    var lineMap = new Dictionary<int, long>();
            //    var count = 0;
            //    using (var streamReader = new StreamReader(path))
            //    {
            //        for (; ; )
            //        {
            //            var position = streamReader.BaseStream.Position;
            //            var line = streamReader.ReadLine();
            //            if (line == null)
            //            {
            //                break;
            //            }
            //            if (count % 1000 == 0)
            //            {
            //                lineMap[count] = position;
            //            }
            //            count++;
            //        }
            //    }
            //    Console.WriteLine($"2 - Found {count} lines in {stopwatch.ElapsedMilliseconds}ms. Mapped-lines={lineMap.Count}");
            //}

            //// Counting lines 3...
            //{
            //    var stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    var path = "../../../../LogFiles/LogFile1.log";
            //    var lines = File.ReadAllLines(path);
            //    Console.WriteLine($"3 - Found {lines.Length} lines in {stopwatch.ElapsedMilliseconds}ms.");
            //}

            // Counting lines 4...
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var path = "../../../../LogFiles/LogFile1.log";

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
        }
    }
}
