using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Domain.Interfaces
{
    public interface IUx
    {
        void ShowMessageBox(string content);
        void ShowToast(string content);
    }
}
