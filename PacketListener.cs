using System;
using System.Net.Sockets;
using System.Text;

namespace Boerman.Aeronautical.AprsClient.Models
{
    public class PacketListener
    {
        private Socket _Socket;

        private class StateObject
        {
            // Client socket.
            public Socket WorkSocket;

            // Size of receive buffer.
            public const int BUFFER_SIZE = 256;

            // Receive buffer.
            public readonly byte[] Buffer = new byte[BUFFER_SIZE];
        }

        public void Start()
        {
            if (_Socket != null)
                Stop();

            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var state = new StateObject { WorkSocket = _Socket };
            _Socket.Connect(AprsConfig.GetConfig().URI, AprsConfig.GetConfig().Port);

            // Begin receiving the data from the remote device.
            _Socket.BeginReceive(state.Buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(ReceiveCallback), state);
            _Socket.Send(Encoding.ASCII.GetBytes(string.Format("user {0} pass {1} vers experimenting 0.1 filter {2}\n", AprsConfig.GetConfig().Callsign, AprsConfig.GetConfig().Password, AprsConfig.GetConfig().Filter)));
        }

        public void Stop()
        {
            if (_Socket != null && _Socket.Connected)
                _Socket.Close();
            _Socket = null;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            var state = (StateObject)ar.AsyncState;

            if (!state.WorkSocket.Connected)
                return;

            var client = state.WorkSocket;

            // Read data from the remote device.
            var bytesRead = client.EndReceive(ar);
            client.BeginReceive(state.Buffer, 0, StateObject.BUFFER_SIZE, 0,
                new AsyncCallback(ReceiveCallback), state);

            if (bytesRead > 0)
            {
                // There might be more data, so store the data received so far.
                var data = Encoding.ASCII.GetString(state.Buffer, 0, bytesRead);


                if (data.StartsWith("#"))
                    return;

                PacketInfo packetInfo;
                try
                {
                    packetInfo = new PacketInfo(data);
                }
                catch (Exception)
                {
                    //TODO: log/show an error
                    return;
                }

                if (PacketReceived != null)
                    PacketReceived(this, new PacketInfoEventArgs(packetInfo));
            }
        }


        public event EventHandler<PacketInfoEventArgs> PacketReceived;
    }
}