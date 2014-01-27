using MultiPlatform.Domain.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Domain.Interfaces
{
    public interface INetwork
    {
        event EventHandler<MultiPlatform.Domain.Code.InternetConnectionChangedEventArgs> InternetConnectionChanged;
        bool IsConnected { get; }
    }
}
