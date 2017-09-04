using System;
using System.Text;

namespace Boerman.Aeronautics.AprsClient.Models
{
    public class Heading
    {
        public Heading(short degrees, short minutes, double seconds)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
        }

        public short Degrees { get; set; }
        public short Minutes { get; set; }
        public double Seconds { get; set; }

        public double ToDegrees()
        {
            return Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
        }

        public void SetDegrees(double degrees)
        {
            Degrees = (short)Math.Floor(degrees);
            var remainder = degrees - Degrees;

            Minutes = (short)Math.Floor(remainder * 60);
            remainder = (remainder * 60) - Minutes;

            Seconds = remainder * 60;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Degrees).Append("°");

            if (Minutes > 0 || Seconds > 0)
                sb.Append(' ').Append(Minutes).Append('\'');

            if (Seconds > 0)
                sb.Append(' ').Append(Seconds.ToString("0.##")).Append("''");

            return sb.ToString();
        }
    }
}