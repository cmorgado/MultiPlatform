using MultiPlatform.Domain.Code;
using System;
namespace MultiPlatform.Domain.Interfaces
{
   public interface IPeerConnector
    {
        event EventHandler<ConnectionStatusChangedEventArgs> ConnectionStatusChanged;
        event EventHandler<DataReceivedEventArgs> DataReceived;
        void SendDataAsync(byte[] dataBytes);
        void StartConnect();
        void StopConnect();
    }
}
