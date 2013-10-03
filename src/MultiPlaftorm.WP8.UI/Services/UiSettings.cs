using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cimbalino.Phone.Toolkit.Services;
using MultiPlatform.Domain.Code;

namespace MultiPlatform.WP8.UI.Services
{
  public  class UiSettings:Domain.Interfaces.ISettings
    {
        public T GetAppSettingValue<T>(string Key)
        {
            string val = string.Empty;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            var pushsett = settings.Any(o => o.Key == Constants.SETTINGS_KEY_PREFIX + Key);

            if (!pushsett)
            {

                return default(T);
            }
            else
            {
                string v = settings[Constants.SETTINGS_KEY_PREFIX + Key].ToString();
                return v.FromJson<T>();
            }
        }

        public void SaveAppSettingValue(string Key, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Any(o => o.Key == Constants.SETTINGS_KEY_PREFIX + Key))

                settings.Add(Constants.SETTINGS_KEY_PREFIX + Key, value.ToJson());
            else
                settings[Constants.SETTINGS_KEY_PREFIX + Key] = value.ToJson();
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
