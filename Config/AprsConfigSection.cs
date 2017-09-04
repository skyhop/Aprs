using System;
using System.Configuration;

namespace Boerman.Aeronautics.AprsClient.Config
{
    public class AprsConfig : ConfigurationSection
    {
        [ConfigurationProperty("uri", DefaultValue = "noam.aprs2.net", IsRequired = false)]
        public string Uri => this["uri"] as string;

        [ConfigurationProperty("port", DefaultValue = 14580, IsRequired = false)]
        public int Port => (int)this["port"];

        [ConfigurationProperty("callsign", DefaultValue = "0", IsRequired = false)]
        public string Callsign => this["callsign"] as string;

        [ConfigurationProperty("password", DefaultValue = "-1", IsRequired = false)]
        public string Password => this["password"] as string;

        [ConfigurationProperty("filter", DefaultValue = "t/poimqstunw", IsRequired = false)]
        public string Filter => this["filter"] as string;

        [ConfigurationProperty("useOgnAdditives", DefaultValue = true, IsRequired = false)]
        public bool UseOgnAdditives => Convert.ToBoolean(this["useOgnAdditives"]);


        public static AprsConfig GetConfig()
        {
            return ConfigurationManager.GetSection("AprsConfig") as AprsConfig;
        }
    }
}
