
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Domain.Code
{

    public class InternetConnectionChangedEventArgs : EventArgs
    {
        public InternetConnectionChangedEventArgs(bool isConnected)
        {
            this.isConnected = isConnected;
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
        }
    }
      

    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Bytes { get;  set; }
    }

    public class ConnectionStatusChangedEventArgs : EventArgs
    {
        public ConnectionStatus Status { get; set; }
    }

    public enum ConnectionStatus
    {
        PeerFound,
        Completed,
        Canceled,
        Failed,
        ReadyForTap,
        Disconnected,
        TapNotSupported,
        Connecting
    }
}
