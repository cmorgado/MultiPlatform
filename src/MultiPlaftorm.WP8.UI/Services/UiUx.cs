using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if NETFX_CORE
using Windows.UI.Notifications;
#endif

namespace MultiPlatform.Shared.Services
{
    public class UiUx : MultiPlatform.Domain.Interfaces.IUx
    {

#if WINDOWS_PHONE
        public void ShowMessageBox(string content)
        {
            MessageBox.Show(content);

        }
#elif NETFX_CORE
        public async void ShowMessageBox(string content)
        {
            var m = new Windows.UI.Popups.MessageDialog(content);
            await m.ShowAsync(); 
        }
#endif


#if WINDOWS_PHONE
        public void ShowToast(string content)
        {
            ToastPrompt toast = new ToastPrompt();
            toast.Title = "";
            toast.Message = "content";
            toast.Show();
        }
#elif NETFX_CORE
        public async void ShowToast(string content)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            template.GetElementsByTagName("text")[0].InnerText = content;

            ToastNotification t = new ToastNotification(template);

            ToastNotificationManager.CreateToastNotifier().Show(t);
        
        }
#endif

    }
}
