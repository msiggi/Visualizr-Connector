using System;
using System.Collections.Generic;
using System.IO;
using Visualizr.Connector;

namespace Visualizr.Connectortest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var poster = new Poster();
            poster.PostErrorOccurred += poster_PostErrorOccurred;

            var hostname = System.Net.Dns.GetHostName();

            // Get Processvalues:
            var pvs = poster.GetProcessValuesAsync("visualizrdemo").Result;

            foreach (var pv in pvs)
            {
                Console.WriteLine(string.Concat(pv.Timestamp, " ", pv.Name, " - ", pv.Value));
            }

            while (true)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                var pValues = new List<ProcessValue>();

                foreach (DriveInfo d in allDrives)
                {
                    try
                    {
                        if (d.IsReady)
                        {
                            var pValue = new ProcessValue(hostname, "FreeSpace Drive " + d.Name, (d.TotalFreeSpace / (1024 * 1024 * 1024)).ToString(), "visualizrdemo");
                            pValues.Add(pValue);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                pValues.Add(new ProcessValue(hostname, "Random-Value", new Random().Next(1500).ToString(), "visualizrdemo"));

                poster.PostProcessValuesAsync(pValues).Wait();
                Console.WriteLine("Ready, waiting one Second ..");
                System.Threading.Thread.Sleep(1000);
            }
            Console.ReadKey();
        }

        private static void poster_PostErrorOccurred(object sender, Poster.PostErrorEventArgs e)
        {
            Console.WriteLine(e.PostErrorException);
        }
    }
}