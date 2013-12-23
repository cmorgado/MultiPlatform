using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MultiPlatform.WP8.UI.Views
{
    public partial class Map : Code.BasePage
    {
        Domain.ViewModels.Map vm;
        public Map()
        {
            InitializeComponent();

         

            this.Loaded += Map_Loaded;
        }

        void Map_Loaded(object sender, RoutedEventArgs e)
        {
              vm = this.DataContext as Domain.ViewModels.Map;

            vm.PropertyChanged += CurrentPosition_PropertyChanged;
        }

        void CurrentPosition_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPosition")
                NokiaMap.SetView(new System.Device.Location.GeoCoordinate { Latitude = vm.CurrentPosition.Coordinate.Latitude, Longitude = vm.CurrentPosition.Coordinate.Longitude }, vm.ZoomLevel);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
        }


    }
}