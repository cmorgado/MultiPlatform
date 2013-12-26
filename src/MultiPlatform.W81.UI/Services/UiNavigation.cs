using MultiPlatform.Domain.Interfaces;
using MultiPlatform.W81.UI.Views;
using MultiPlatform.W81.UI.Views.Demo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace MultiPlatform.W81.UI.Services
{
    public class UiNavigation : Domain.Interfaces.INavigation<Domain.Interfaces.NavigationModes>
    {


        private static readonly IDictionary<Type, Type> ViewModelRouting = new Dictionary<Type, Type>()
                                                                  {
                                                                         { typeof(MultiPlatform.Domain.ViewModels.Home),typeof(Home) }
                                                                         ,
                                                                            { typeof(MultiPlatform.Domain.ViewModels.Details),typeof(Details) }
                                                                             ,
                                                                            { typeof(MultiPlatform.Domain.ViewModels.Map),typeof(Map) }
                                                                             ,
                                                                            { typeof(MultiPlatform.Domain.ViewModels.NFCSend),typeof(NFCSend) }
  ,
                                                                            { typeof(MultiPlatform.Domain.ViewModels.SQLite),typeof(SQLiteDemo) }

                                                                  };




        void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {



            switch (e.NavigationMode)
            {
                case NavigationMode.Back:
                    navigationType = NavigationModes.Back;
                    break;
                case NavigationMode.Forward:
                    navigationType = NavigationModes.Forward;
                    break;
                case NavigationMode.New:
                    navigationType = NavigationModes.New;
                    break;
                case NavigationMode.Refresh:
                    navigationType = NavigationModes.Refresh;
                    break;

                default:
                    break;
            }
        }

        public void Navigate<TDestinationViewModel>(bool sameContext, object parameter = null)
        {
            Navigate<TDestinationViewModel>(parameter);
        }


        public bool CanGoBack
        {
            get
            {
                return RootFrame.CanGoBack;
            }
        }

        /// <summary>
        /// Gets the root frame.
        /// </summary>
        private static Frame RootFrame
        {
            get { return Window.Current.Content as Frame; }
        }


        public static TJson DecodeNavigationParameter<TJson>(NavigationEventArgs args)
        {
            try
            {
                var param = args.Parameter;
                return (TJson)param;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(TJson);
            }
        }


        public void GoBack()
        {
            RootFrame.GoBack();
        }

        public void Navigate<TDestinationViewModel>(object parameter)
        {
            var dest = ViewModelRouting[typeof(TDestinationViewModel)];

            RootFrame.Navigate(dest, parameter);
        }

        private NavigationModes navigationType;
        public NavigationModes NavigationType { get { return navigationType; } }
    }
}
