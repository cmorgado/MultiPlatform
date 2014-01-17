using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MultiPlatform.W81.UI.Code
{
    public class BasePage : Page
    {
        public BasePage()
        {
            this.Loaded += page_Loaded;
            this.Unloaded += page_Unloaded;
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Window_SizeChanged;
            DetermineVisualState();
        }

        private void page_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = this.DataContext as Domain.ViewModels.BaseViewModel;
            string p = e.Parameter as string;
            viewModel.navigationParameterJson = p;
            viewModel.CanGoBack = this.Frame.CanGoBack;
            switch (e.NavigationMode)
            {
                case Windows.UI.Xaml.Navigation.NavigationMode.Back:
                    viewModel.NavigationType = Domain.Interfaces.NavigationModes.Back;
                    break;
                case Windows.UI.Xaml.Navigation.NavigationMode.Forward:
                    viewModel.NavigationType = Domain.Interfaces.NavigationModes.Forward;
                    break;
                case Windows.UI.Xaml.Navigation.NavigationMode.New:
                    viewModel.NavigationType = Domain.Interfaces.NavigationModes.New;
                    break;
                case Windows.UI.Xaml.Navigation.NavigationMode.Refresh:
                    viewModel.NavigationType = Domain.Interfaces.NavigationModes.Refresh;
                    break;

                default:
                    break;
            }

            base.OnNavigatedTo(e);
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            DetermineVisualState();
        }

        private void DetermineVisualState()
        {
            var state = string.Empty;
            var applicationView = ApplicationView.GetForCurrentView();
            var size = Window.Current.Bounds;

            if (applicationView.IsFullScreen)
            {
                if (applicationView.Orientation == ApplicationViewOrientation.Landscape)
                    state = "FullScreenLandscape";
                else
                    state = "FullScreenPortrait";
            }
            else
            {
                if (size.Width == 320)
                    state = "Snapped";
                else if (size.Width <= 500)
                    state = "Narrow";
                else
                    state = "Filled";
            }

            System.Diagnostics.Debug.WriteLine("Width: {0}, New VisulState: {1}", size.Width, state);

            VisualStateManager.GoToState(this, state, true);
        }
    }
}
