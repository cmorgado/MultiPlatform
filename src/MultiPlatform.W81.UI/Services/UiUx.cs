using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Media;
using Windows.UI;



namespace MultiPlatform.W81.UI.Services
{
    public class UiUx : MultiPlatform.Domain.Interfaces.IUx
    {


        public async void ShowMessageBox(string content)
        {
            var m = new Windows.UI.Popups.MessageDialog(content);
            await m.ShowAsync();
        }



        public async void ShowToast(string content)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            template.GetElementsByTagName("text")[0].InnerText = content;

            ToastNotification t = new ToastNotification(template);

            ToastNotificationManager.CreateToastNotifier().Show(t);

        }



        public void GoToLogin()
        {

            Windows.UI.Xaml.Controls.SettingsFlyout settings = new Windows.UI.Xaml.Controls.SettingsFlyout();
            settings.Width = 500;
            settings.HeaderBackground = new SolidColorBrush(Color.FromArgb(255, 0, 113, 206));
            settings.HeaderForeground = new SolidColorBrush(Colors.White);
            settings.Title = International.Translation.Login_Title;
            settings.Content = new MultiPlatform.W81.UI.Views.UC.Login();
            settings.Show();


        }

        public void DoLogin()
        {
            var f = Window.Current.Content as Frame;
            f.Navigate(f.Content.GetType());
            f.GoBack();
        }


    }
}
