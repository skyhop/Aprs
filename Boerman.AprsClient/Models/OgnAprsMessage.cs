namespace Boerman.AprsClient.Models
{
    public class OgnAprsMessage : AprsMessage
    {
        public double Climbrate { get; internal set; }
        public double Turnrate { get; internal set; }
    }
}
