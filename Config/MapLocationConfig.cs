using System;
using System.Configuration;

namespace Boerman.Aeronautics.AprsClient.Config
{
    public class MapLocationConfig : ConfigurationElement
    {
        [ConfigurationProperty("latitude", IsRequired = true)]
        public double Latitude => Convert.ToDouble(this["latitude"]);

        [ConfigurationProperty("longitude", IsRequired = true)]
        public double Longitude => Convert.ToDouble(this["longitude"]);

        [ConfigurationProperty("altitude", IsRequired = true)]
        public double Altitude => Convert.ToDouble(this["altitude"]);
    }
}
