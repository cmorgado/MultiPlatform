using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlatform.Domain.Code;
using Windows.ApplicationModel;

namespace MultiPlatform.W8.UI.Services
{
    public class UiSettings : Domain.Interfaces.ISettings
    {

        public T GetAppSettingValue<T>(string Key)
        {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var roamingSettings = applicationData.RoamingSettings;

            if (roamingSettings.Values.ContainsKey("SETTINGS_" + Key))
            {
                return roamingSettings.Values["SETTINGS_" + Key].ToString().FromJson<T>();
            }
            else
                return default(T);
        }

        public void SaveAppSettingValue(string Key, object value)
        {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var roamingSettings = applicationData.RoamingSettings;

            if (roamingSettings.Values.ContainsKey("SETTINGS_" + Key))
            {
                roamingSettings.Values["SETTINGS_" + Key] = value.ToJson();
            }
            else
            {
                roamingSettings.Values.Add("SETTINGS_" + Key, value.ToJson());
            }
        }


        public string AppVersion()
        {
            PackageVersion version = Package.Current.Id.Version;
            return string.Format("{0}.{1}", version.Major, version.Minor);
        }
    }
}
