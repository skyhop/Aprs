using System;
using Boerman.Aeronautics.AprsClient.Enums;

namespace Boerman.Aeronautics.AprsClient.Models
{
    public class Altitude
    {
        private const int BaselineDepthBelowSealevel = 10000;
        private const double MeterToFootConversionFactor = 3.2808399;

        private AltitudeUnit _altitudeUnit;
        private int _magnitude;

        private Altitude()
        {
            
        }

        public static Altitude FromMetersAboveBaseline(int metersAboveBaseline)
        {
            return new Altitude
            {
                _altitudeUnit = AltitudeUnit.Meters, 
                _magnitude = metersAboveBaseline - BaselineDepthBelowSealevel
            };
        }

        public static Altitude FromMetersAboveSeaLevel(int metersAboveSeaLevel)
        {
            return new Altitude
            {
                _altitudeUnit = AltitudeUnit.Meters,
                _magnitude = metersAboveSeaLevel
            };
        }
        public static Altitude FromFeetAboveSeaLevel(int feetAboveSeaLevel)
        {
            return new Altitude
            {
                _altitudeUnit = AltitudeUnit.Feet,
                _magnitude = feetAboveSeaLevel
            };
        }

        public int MetersAboveBaseline => MetersAboveSeaLevel + BaselineDepthBelowSealevel;

        public int MetersAboveSeaLevel => _altitudeUnit == AltitudeUnit.Meters
            ? _magnitude
            : Convert.ToInt32(_magnitude / MeterToFootConversionFactor);

        public int FeetAboveSeaLevel => _altitudeUnit == AltitudeUnit.Feet
            ? _magnitude
            : Convert.ToInt32(_magnitude * MeterToFootConversionFactor);

        public override string ToString()
        {
            return $"{FeetAboveSeaLevel:d}ft MSL";
        }
    }
}