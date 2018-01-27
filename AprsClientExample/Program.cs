using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Boerman.AprsClient;
using Boerman.AprsClient.Models;

namespace AprsClientExample
{
    /// <summary>
    /// This program will connect to the OGN APRS servers and show any APRS 
    /// packets flowing through. The packets will also be saved in a file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary> 
        /// <param name="args">The command-line arguments.</param>
        static async Task Main(string[] args)
        {
            Console.Title = "APRS Client Example";

            Listener listener = new Listener(new Config() {
                Callsign = @"debugging",
                Password = "",
                Uri = "aprs.glidernet.org",
                UseOgnAdditives = true,
                Port = 10152
            });

            listener.Disconnected += (sender, e) => {
                Console.WriteLine(" -- Disconnected");
            };

            StreamWriter w = File.AppendText("positions.txt");
            w.AutoFlush = true;

            listener.DataReceived += (sender, e) => {
                AprsMessage message = null;

                try {
                    message = PacketInfo.Parse(e.Data);
                } catch (Exception ex) {
                    
                }

                if (message == null) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Data);
                    Console.ResetColor();
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(e.Data);
                    Console.ResetColor();
                }
            };

            //listener.PacketReceived += (sender, eventArgs) =>
            //{
            //    // Please note it is faster to use the `listener.DataReceived` event if you only want the raw data.
            //    Console.WriteLine(eventArgs.AprsMessage.RawData);
            //    w.Write($"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)} {eventArgs.AprsMessage.RawData}");
            //};

            await listener.Open();

            Console.ReadKey();

            listener.Stop();
        }
    }
}
