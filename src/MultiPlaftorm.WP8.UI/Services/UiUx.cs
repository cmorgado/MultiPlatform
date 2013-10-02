using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



#if NETFX_CORE
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Callisto.Controls;


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
            toast.Message = content;
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

#if WINDOWS_PHONE
        public void GoToLogin()
        {
            MultiPlatform.WP8.UI.Services.UiNavigation nav = new WP8.UI.Services.UiNavigation();
            nav.Navigate<Domain.ViewModels.Login>(false);
        }
#elif NETFX_CORE
        public void GoToLogin()
        {
            Callisto.Controls.SettingsFlyout AboutFlyout = new Callisto.Controls.SettingsFlyout();


            AboutFlyout.FlyoutWidth = Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth.Wide;
            AboutFlyout.HeaderText = International.Translation.Login_Title;
            AboutFlyout.Content = new MultiPlatform.W8.UI.Views.UC.Login();
            AboutFlyout.IsOpen = true;




        }
#endif

#if WINDOWS_PHONE
        public void DoLogin()
        {
            
            MultiPlatform.WP8.UI.Services.UiNavigation nav = new WP8.UI.Services.UiNavigation();
            nav.GoBack();
        }
#elif NETFX_CORE
        public void DoLogin()
        {
            var f = Window.Current.Content as Frame;
            f.Navigate(f.Content.GetType());
            f.GoBack();
        }
#endif

    }
}
