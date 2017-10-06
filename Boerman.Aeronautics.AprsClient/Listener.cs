/*
 * ToDo: log some usable information on exceptions
 */

using System;
using System.Diagnostics;
using System.Net;
using Boerman.Aeronautics.AprsClient.Models;
using Boerman.TcpLib.Client;

namespace Boerman.Aeronautics.AprsClient
{
    public class Listener : TcpClient<string, string>
    {
        public Listener() : base(new ClientSettings()
        {
            EndPoint = new DnsEndPoint(
                    AprsConfig.Uri,
                    AprsConfig.Port),
            Listening = false,
            Splitter = "\r\n",
            Timeout = 1020000, // 17 minutes afaik,
            ReconnectOnDisconnect = true
        })
        {
            // Set the callbacks before starting the client
            ConnectEvent += OnConnect;
            ReceiveEvent += OnReceive;
            
            Start();
        }

        private void OnConnect()
        {
            Send(
                $"user {AprsConfig.Callsign} pass {AprsConfig.Password} vers experimenting 0.1 filter {AprsConfig.Filter}\n");
        }

        private void OnReceive(string data)
        {
            if (String.IsNullOrEmpty(data)) return;

            DataReceived?.Invoke(this, new AprsDataReceivedEventArgs(data));

            if (PacketReceived == null) return;

            AprsMessage packetInfo;

            try
            {
                packetInfo = PacketInfo.Parse(data);
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
