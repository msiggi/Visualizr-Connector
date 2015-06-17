using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Visualizr.Connector;

namespace Visualizr.ConnectortestPerNuget
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var poster = new Poster();
            var hostname = System.Net.Dns.GetHostName();

            poster.PostErrorOccurred += Poster_PostErrorOccurred;

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

            var state = poster.PostProcessValuesAsync(pValues);
            Console.WriteLine("Fertig");
            Console.ReadKey();
        }

        private static void Poster_PostErrorOccurred(object sender, Poster.PostErrorEventArgs e)
        {
            Console.WriteLine(e.PostErrorException);
        }
    }
}