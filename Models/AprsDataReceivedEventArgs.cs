namespace Boerman.Aeronautics.AprsClient.Models
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
