using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Boerman.AprsClient
{
    public static class AprsConfig
    {
        public static IConfigurationRoot Configuration { get; set; }

        static AprsConfig() {
			Configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true).Build();
        }

        public static string Uri => Configuration["aprsClient:uri"] ?? "noam.aprs2.net";
        public static int Port => Convert.ToInt32(Configuration["aprsClient:port"] ?? "14580");
        public static string Callsign => Configuration["aprsClient:callsign"] ?? "0";
        public static string Password => Configuration["aprsClient:password"] ?? "-1";
        public static string Filter => Configuration["aprsClient:filter"] ?? "t/poimqstunw";
        public static bool UseOgnAdditives => Convert.ToBoolean(Configuration["aprsClient:useOgnAdditives"] ?? "true");
    }

    public class Config {
        string uri;
        int? port;
        string callsign;
        string password;
        string filter;
        bool? useOgnAdditives;

        public string Uri
        {
            get { return uri ?? AprsConfig.Uri; }
            set { uri = value; }
        }

        public int Port
        {
            get { return port ?? AprsConfig.Port; }
            set { port = value; }
        }

        public string Callsign
        {
            get { return callsign ?? AprsConfig.Callsign; }
            set { callsign = value; }
        }

        public string Password
        {
            get { return password ?? AprsConfig.Password; }
            set { password = value; }
        }

        public string Filter
        {
            get { return filter ?? AprsConfig.Filter; }
            set { filter = value; }
        }

        public bool UseOgnAdditives
        {
            get { return useOgnAdditives ?? AprsConfig.UseOgnAdditives; }
            set { useOgnAdditives = value; }
        }
    }
}
