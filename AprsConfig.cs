using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Boerman.Aeronautics.AprsClient
{
    public static class AprsConfig
    {
        public static IConfigurationRoot Configuration { get; set; }

        static AprsConfig() {
			Configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json").Build();
        }

        public static string Uri => Configuration["aprsClient:uri"] ?? "noam.aprs2.net";
        public static int Port => Convert.ToInt32(Configuration["aprsClient:port"] ?? "14580");
        public static string Callsign => Configuration["aprsClient:callsign"] ?? "0";
        public static string Password => Configuration["aprsClient:password"] ?? "-1";
        public static string Filter => Configuration["aprsClient:filter"] ?? "t/poimqstunw";
        public static bool UseOgnAdditives => Convert.ToBoolean(Configuration["aprsClient:useOgnAdditives"] ?? "true");
    }
}
