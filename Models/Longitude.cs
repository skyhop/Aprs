using System;
using Boerman.Aeronautics.AprsClient.Enums;

namespace Boerman.Aeronautics.AprsClient.Models
{
    public class Longitude : LatitudeLongitudeBase
    {
        public Longitude(short degrees, short minutes, LongitudeHemisphere hemisphere)
            : this(degrees, minutes, 0, hemisphere)
        {

        }

        public Longitude(short degrees, double minutes, LongitudeHemisphere hemisphere)
            : base(hemisphere == LongitudeHemisphere.East ? PositionSign.Positive : PositionSign.Negative, degrees, (short)Math.Floor(minutes), (minutes - Math.Floor(minutes)) * 60)
        {
        }

        public Longitude(short degrees, short minutes, double seconds, LongitudeHemisphere hemisphere)
            : base(hemisphere == LongitudeHemisphere.East ? PositionSign.Positive : PositionSign.Negative, degrees, minutes, seconds)
        {
        }

        public Longitude(short degrees, short minutes, double seconds, LongitudeHemisphere hemisphere, short ambiguity)
            : base(hemisphere == LongitudeHemisphere.East ? PositionSign.Positive : PositionSign.Negative, degrees, minutes, seconds, ambiguity)
        {
        }

        public LongitudeHemisphere Hemisphere => Value > 0 ? LongitudeHemisphere.East : LongitudeHemisphere.West;

        public override string ToString()
        {
            return Vector + " " + (Hemisphere == LongitudeHemisphere.East? "E" : "W");
        }
    }
}