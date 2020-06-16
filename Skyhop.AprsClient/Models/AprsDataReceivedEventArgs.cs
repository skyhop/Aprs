namespace Skyhop.Aprs.Client.Models
{
    public class AprsDataReceivedEventArgs
    {
        public AprsDataReceivedEventArgs(string data)
        {
            Data = data;
        }

        public string Data { get; set; }
    }
}
