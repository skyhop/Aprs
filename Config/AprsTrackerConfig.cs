using System.Configuration;

namespace Boerman.Aeronautics.AprsClient.Config
{
    public class AprsTrackerConfig : ConfigurationSection
    {
        [ConfigurationProperty("startLocation", IsRequired = true)]
        public MapLocationConfig StartLocation => this["startLocation"] as MapLocationConfig;

        public static AprsTrackerConfig GetConfig()
        {
            return ConfigurationManager.GetSection("AprsTrackerConfig") as AprsTrackerConfig;
        }
    }
}
