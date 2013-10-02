using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Windows.Devices.Geolocation;
using MultiPlatform.Shared.Services;
using System.Device.Location;

namespace MultiPlatform.WP8.UI.Code
{

    public class GeoCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
           object parameter, CultureInfo culture)
        {
            if (value != null)
            {

                var loc = value as Domain.Models.Ui.GeoPosition;
                if (loc.Coordinate != null)
                {
                    GeoCoordinate g = new GeoCoordinate();

                    g.Longitude = loc.Coordinate.Longitude;
                    g.Latitude = loc.Coordinate.Latitude;
                    if (loc.Coordinate.Altitude.HasValue)
                        g.Altitude = loc.Coordinate.Altitude.Value;

                    return g;
                }

                return new GeoCoordinate(40.712923, -74.013292);
            }
            else
                return new GeoCoordinate(40.712923, -74.013292);



        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
