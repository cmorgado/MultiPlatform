using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlatform.Domain.Code;
using Cimbalino.Phone.Toolkit.Services;

namespace MultiPlatform.WP8.UI.Services
{
  public  class UiSettings:Domain.Interfaces.ISettings
    {
        public T GetAppSettingValue<T>(string Key)
        {
            string val = string.Empty;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            var pushsett = settings.Any(o => o.Key == "SETTINGS_" + Key);

            if (!pushsett)
            {

                return default(T);
            }
            else
            {
                string v = settings["SETTINGS_" + Key].ToString();
                return v.FromJson<T>();
            }
        }

        public void SaveAppSettingValue(string Key, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Any(o => o.Key == "SETTINGS_" + Key))

                settings.Add("SETTINGS_" + Key, value.ToJson());
            else
                settings["SETTINGS_" + Key] = value.ToJson();
        }

        public string AppVersion()
        {
            ApplicationManifestService manifest = new ApplicationManifestService();
            return manifest.GetApplicationManifest().App.Version;
        }

        public bool IsConnectedToInternet()
        {
            return Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
