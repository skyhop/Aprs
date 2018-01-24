/*
 * ToDo: log some usable information on exceptions
 */

using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Boerman.AprsClient.Models;
using Boerman.Networking;

namespace Boerman.AprsClient
{
    public class Listener : TcpClient
    {
        private Config _config;
        private bool _shouldBeConnected = false;
        private Timer _timer;

        public Listener(Config config = null) : base() {
            _config = config ?? new Config();
            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _timer.Elapsed += (sender, e) => Send("#");

            // Assign some event listeners
            Connected += (sender, e) => {
                Send($"user {_config.Callsign} pass {_config.Password} vers bocobrew filter {_config.Filter}\n");
            };

            Disconnected += async (sender, e) => {
                // Unless the disconnect method has been called, reconnect to the server
                if (_shouldBeConnected) await Start();
            };

            Received += Handle_Received;
        }

        /// <summary>
        /// Connect to the APRS server
        /// </summary>
        public async Task Start() {
            _shouldBeConnected = true;

            bool isConnected = false;
            while (!isConnected) {
                isConnected = await base.Open(new DnsEndPoint(_config.Uri, _config.Port));
            }

            _timer.Start();
        }

        /// <summary>
        /// Close the connection to the server
        /// </summary>
        public void Stop() {
            _shouldBeConnected = false;
            _timer.Stop();
            base.Close();
        }

        void Handle_Received(object sender, ReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data)) return;

            DataReceived?.Invoke(this, new AprsDataReceivedEventArgs(e.Data));

            if (PacketReceived == null) return;

            AprsMessage packetInfo;

            try
            {
                packetInfo = PacketInfo.Parse(e.Data);
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

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;
        public event EventHandler<AprsDataReceivedEventArgs> DataReceived;
    }
}
