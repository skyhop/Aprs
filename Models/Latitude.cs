using System;
using Boerman.Aeronautics.AprsClient.Enums;

namespace Boerman.Aeronautics.AprsClient.Models
{
    public class Latitude : LatitudeLongitudeBase
    {
        public Latitude(double degrees)
            : this(Convert.ToInt16(Math.Floor(Math.Abs(degrees))), 
                Math.Abs(degrees) - Math.Abs(Convert.ToInt16(Math.Floor(degrees))), 
                degrees >= 0 ? LatitudeHemisphere.North : LatitudeHemisphere.South)
        {

        }

        public Latitude(short degrees, short minutes, LatitudeHemisphere hemisphere)
            : this(degrees, minutes, 0, hemisphere)
        {

        }

        public Latitude(short degrees, double minutes, LatitudeHemisphere hemisphere)
            : base(hemisphere == LatitudeHemisphere.North ? PositionSign.Positive : PositionSign.Negative, degrees, (short)Math.Floor(minutes), (minutes - Math.Floor(minutes)) * 60)
        {
        }

        public Latitude(short degrees, short minutes, double seconds, LatitudeHemisphere hemisphere)
            : base(hemisphere == LatitudeHemisphere.North ? PositionSign.Positive : PositionSign.Negative, degrees, minutes, seconds)
        {
        }

        public Latitude(short degrees, short minutes, double seconds, LatitudeHemisphere hemisphere, short ambiguity)
            : base(hemisphere == LatitudeHemisphere.North ? PositionSign.Positive : PositionSign.Negative, degrees, minutes, seconds, ambiguity)
        {
        }

        public LatitudeHemisphere Hemisphere => Value > 0 ? LatitudeHemisphere.North : LatitudeHemisphere.South;

        public override string ToString()
        {
            return Vector + " " + (Hemisphere == LatitudeHemisphere.North ? "N" : "S");
        }
    }
}