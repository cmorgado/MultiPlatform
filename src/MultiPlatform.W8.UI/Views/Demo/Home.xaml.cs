using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace MultiPlatform.W8.UI.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Home : Code.BasePage
    {
        public Home()
        {
            this.InitializeComponent();
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            String output = String.Format(
                               "Name: \"{0}\"\n" +
                               "Version: {1}.{2}.{3}.{4}\n" +
                               "Architecture: {5}\n" +
                               "ResourceId: \"{6}\"\n" +
                               "Publisher: \"{7}\"\n" +
                               "PublisherId: \"{8}\"\n" +
                               "FullName: \"{9}\"\n" +
                               "FamilyName: \"{10}\"\n" +
                               "IsFramework: {11}",
                               packageId.Name,
                               version.Major, version.Minor, version.Build, version.Revision,
                               packageId.Architecture,
                               packageId.ResourceId,
                               packageId.Publisher,
                               packageId.PublisherId,
                               packageId.FullName,
                               packageId.FamilyName,
                               package.IsFramework);

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
    }
}
