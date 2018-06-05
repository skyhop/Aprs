/*
 * ToDo: log some usable information on exceptions
 */

using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Boerman.AprsClient.Models;
using Boerman.Networking;

namespace Boerman.AprsClient
{
    // ToDo: Can we hide the underlying Open method?
    public class Listener : TcpClient
    {
        private Config _config;
        private bool _shouldBeConnected = false;
        private Timer _timer;

        private StringBuilder stringBuffer = new StringBuilder();

        public Listener(Config config = null) : base() {
            _config = config ?? new Config();
            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _timer.Elapsed += (sender, e) => Send("#");

            // Assign some event listeners
            Connected += (sender, e) => {
                Send($"user {_config.Callsign} pass {_config.Password} vers {_config.SoftwareName} {_config.SoftwareVersion} filter {_config.Filter}\n");
            };

            Disconnected += async (sender, e) => {
                // Unless the disconnect method has been called, reconnect to the server
                if (_shouldBeConnected) await Open();
            };

            Received += Handle_Received;
        }

        /// <summary>
        /// Connect to the APRS server
        /// 
        /// The APRS client will automatically reconnect in case the connection is lost.
        /// </summary>
        public async Task<bool> Open() {
            return await Open(new DnsEndPoint(_config.Uri, _config.Port));
        }

        /// <summary>
        /// Connect to the APRS server. By providing the endpoint to connect with you automatically override the configuration as supplied in the Config object.
        /// 
        /// The APRS client will automatically reconnect in case the connection is lost.
        /// </summary>
        public async Task<bool> Open(EndPoint endpoint) {
            _shouldBeConnected = true;

            if (!_config.ValidateConfiguration())
            {
                Trace.TraceWarning("Configuration validation resulted in one or more errors. See trace for more information.");
            }

            bool isConnected = false;
            while (!isConnected)
            {
                isConnected = await base.Open(endpoint);
            }

            _timer.Start();

            return true;
        }

        /// <summary>
        /// Close the connection to the APRS server. Same as `Stop()`
        /// </summary>
        public new void Close() {
            Stop();
        }

        /// <summary>
        /// Close the connection to the APRS server. Same as `Close()`
        /// </summary>
        public void Stop() {
            _shouldBeConnected = false;
            _timer.Stop();
            base.Close();
        }

        void Handle_Received(object sender, ReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data)) return;

            string content;
            string[] chunks;
            bool endsWithNewLine;

            lock (stringBuffer)
            {
                stringBuffer.Append(e.Data);
                content = stringBuffer.ToString();

                // ToDo: Test this thing with multiple cultures. Maybe just fix the /r/n split?
                endsWithNewLine = content.EndsWith(Environment.NewLine, StringComparison.Ordinal);

                stringBuffer.Clear();

                chunks = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // Add the last part of previous message to the buffer
                if (!endsWithNewLine) stringBuffer.Append(chunks.Length - 1);
            }

            var parts = chunks.Length - (endsWithNewLine ? 0 : 1);

            for (int i = 0; i < parts; i++) {
                string message = chunks[i];

                DataReceived?.Invoke(this, new AprsDataReceivedEventArgs(message));

                if (PacketReceived == null) return;

                AprsMessage packetInfo;

                try
                {
                    packetInfo = PacketInfo.Parse(message);
                }
                catch (Exception ex)
                {
                    Trace.TraceInformation(ex.ToString());
                    return;
                }

                // Usually the case when the packet is corrupt or something like that.
                if (packetInfo == null) return;

                PacketReceived.Invoke(this, new PacketReceivedEventArgs(packetInfo));
            }
        }

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;
        public event EventHandler<AprsDataReceivedEventArgs> DataReceived;
    }
}
