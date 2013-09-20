using MultiPlatform.Domain.Code;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace MultiPlatform.WP8.UI.Services
{
    public class UiNavigation : Domain.Interfaces.INavigation<Domain.Interfaces.NavigationModes>
    {
        public static readonly Dictionary<Type, string> ViewModelRouting = new Dictionary<Type, string>
                                                                    {
                                                                          {
                                                                              typeof(Domain.ViewModels.Home), "/Views/Home.xaml"
                                                                          },
                                                                          {
                                                                              typeof(Domain.ViewModels.Details), "/Views/Details.xaml"
                                                                          }
                                                                     };


        void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {



            switch (e.NavigationMode)
            {
                case System.Windows.Navigation.NavigationMode.Back:
                    navigationType = Domain.Interfaces.NavigationModes.Back;
                    break;
                case System.Windows.Navigation.NavigationMode.Forward:
                    navigationType = Domain.Interfaces.NavigationModes.Forward;
                    break;
                case System.Windows.Navigation.NavigationMode.New:
                    navigationType = Domain.Interfaces.NavigationModes.New;
                    break;
                case System.Windows.Navigation.NavigationMode.Refresh:
                    navigationType = Domain.Interfaces.NavigationModes.Refresh;
                    break;
                case System.Windows.Navigation.NavigationMode.Reset:
                    navigationType = Domain.Interfaces.NavigationModes.Refresh;
                    break;
                default:
                    break;
            }
        }

        private Frame RootFrame
        {
            get { return Application.Current.RootVisual as Frame; }
        }


        public bool CanGoBack
        {
            get
            {
                return RootFrame.CanGoBack;
            }
        }


        public void GoBack()
        {
            RootFrame.GoBack();
        }


        public void Navigate<TDestinationViewModel>(object parameter)
        {
            var navParameter = string.Empty;
            if (parameter != null)
            {
                navParameter = "?param=" +  parameter.ToJson();
            }

            if (ViewModelRouting.ContainsKey(typeof(TDestinationViewModel)))
            {
                var page = ViewModelRouting[typeof(TDestinationViewModel)];

                this.RootFrame.Navigate(new Uri("/" + page + navParameter, UriKind.Relative));
            }
        }

        public void Navigate<TDestinationViewModel>(bool sameContext, object parameter)
        {
            if (sameContext)
            {
                // set animation
            }
            else
            {
                //set animation
            }
            Navigate<TDestinationViewModel>(parameter);
        }



        private Domain.Interfaces.NavigationModes navigationType;
        public Domain.Interfaces.NavigationModes NavigationType { get { return navigationType; } }


    }
}
