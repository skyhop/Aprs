using System;
using System.Diagnostics;
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
        public static string SoftwareName => Configuration["aprsClient:softwareName"] ?? "bocobrew";
        public static string SoftwareVersion => Configuration["aprsClient:softwareVersion"] ?? "0.0";
        public static string Filter => Configuration["aprsClient:filter"] ?? "t/poimqstunw";
        public static bool UseOgnAdditives => Convert.ToBoolean(Configuration["aprsClient:useOgnAdditives"] ?? "true");
    }

    public class Config {
        string uri;
        int? port;
        string callsign;
        string password;

        string softwareName;
        string softwareVersion;

        string filter;
        bool? useOgnAdditives;

        // ToDo: Check whether the URI can be used to connect with an IP address.
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

        public string SoftwareName {
            get { return softwareName ?? AprsConfig.SoftwareName; }
            set { softwareName = value; }
        }

        public string SoftwareVersion {
            get { return softwareVersion ?? AprsConfig.SoftwareVersion; }
            set { softwareVersion = value; }
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

        public bool ValidateConfiguration() {
            bool callsignValidity = Helpers.VerifyCallsign(Callsign);
            bool passwordValidity = Helpers.VerifyPassword(Callsign, Password);
            bool checkSoftwareName = SoftwareName.IndexOf(' ') > -1;
            bool checkSoftwareVersion = SoftwareVersion.IndexOf(' ') > -1;

            if (!callsignValidity)
            {
                Trace.TraceWarning($"{nameof(Callsign)} could not be validated, see http://www.aprs-is.net/Connecting.aspx for more information");
            }

            if (!passwordValidity) {
                Trace.TraceWarning($"{nameof(Password)} is not valid for given callsign. Please request a valid password with the software vendor in case you need write access to the APRS server.");
            }

            if (Password == "-1") {
                Trace.TraceWarning("APRS client is in readonly mode. Request a valid password in case you need write access to the APRS server");
            }

            if (!checkSoftwareName) {
                Trace.TraceWarning($"{nameof(SoftwareName)} contains a space.Spaces are illegal. Your mileage may vary now.");
            }

            if (!checkSoftwareVersion) {
                Trace.TraceWarning($"{nameof(SoftwareVersion)} contains a space. Spaces are illegal. Your mileage may vary now.");
            }

            return callsignValidity && passwordValidity && checkSoftwareName && checkSoftwareVersion;
        }
    }
}
