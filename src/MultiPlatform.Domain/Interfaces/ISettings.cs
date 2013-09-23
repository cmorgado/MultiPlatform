using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Interfaces
{
  public  interface ISettings
    {
        T GetAppSettingValue<T>(string Key);
        void SaveAppSettingValue(string Key, object value);
        string AppVersion();
        bool IsConnectedToInternet();
    }
}
