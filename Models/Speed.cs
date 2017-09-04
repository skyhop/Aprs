using System;

namespace Boerman.Aeronautics.AprsClient.Models
{
    public class Speed
    {
        private Speed()
        {
            
        }

        public static Speed FromKnots(double knots)
        {
            return new Speed {Knots = knots};
        }

        public double Knots { get; private set; }

        public double MilesPerHour => Knots * 1.15077945;
        public double KilometersPerHour => Knots * 1.85200;

        public override string ToString()
        {
            return $"{Math.Floor(Knots)}KTS";
        }
    }
}