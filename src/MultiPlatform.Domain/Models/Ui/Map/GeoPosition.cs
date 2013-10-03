using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Models.Ui.Map
{

    public class GeoPositionChangedEventArgs : EventArgs
    {
        public GeoPosition Position { get; private set; }

        public GeoPositionChangedEventArgs(GeoPosition position)
        {
            Position = position;
        }
    }

    public class LocationStatusChangedEventArgs : EventArgs
    {
        public MultiPlatform.Domain.Interfaces.LocationStatus Status { get; private set; }

        public LocationStatusChangedEventArgs(MultiPlatform.Domain.Interfaces.LocationStatus status)
        {
            Status = status;
        }
    }

    public class CivicAdress:ModelBase
    {

        private string _City;
        public string City
        {
            get { return this._City; }
            set
            {
               
                    _City = value;
                    NotifyPropertyChanged();
                
            }
        }


        private string _Country;
        public string Country
        {
            get { return this._Country; }
            set
            {
               
                    _Country = value;
                    NotifyPropertyChanged();
                
            }
        }



        private string _State;
        public string State
        {
            get { return this._State; }
            set
            {
               
                    _State = value;
                    NotifyPropertyChanged();
               
            }
        }



        private string _PostalCode;
        public string PostalCode
        {
            get { return this._PostalCode; }
            set
            {
               
                    _PostalCode = value;
                    NotifyPropertyChanged();
               
            }
        }



        private DateTimeOffset _TimeStamp;
        public DateTimeOffset TimeStamp
        {
            get { return this._TimeStamp; }
            set
            {
               
                    _TimeStamp = value;
                    NotifyPropertyChanged();
               
            }
        }
        

    
    }

    public class Coordinate:ModelBase
    {


        private DateTimeOffset _TimeStamp;
        public DateTimeOffset TimeStamp
        {
            get { return this._TimeStamp; }
            set
            {
              
                    _TimeStamp = value;
                    NotifyPropertyChanged();
               
            }
        }


        private double _Latitude;
        public double Latitude
        {
            get { return this._Latitude; }
            set
            {
               
                    _Latitude = value;
                    NotifyPropertyChanged();
               
            }
        }



        private double _Longitude;
        public double Longitude
        {
            get { return this._Longitude; }
            set
            {
               
                    _Longitude = value;
                    NotifyPropertyChanged();
              
            }
        }


        private double _Accuracy;
        public double Accuracy
        {
            get { return this._Accuracy; }
            set
            {
              
                    _Accuracy = value;
                    NotifyPropertyChanged();
               
            }
        }


        private double? _Altitude;
        public double? Altitude
        {
            get { return this._Altitude; }
            set
            {
               
                    _Altitude = value;
                    NotifyPropertyChanged();
               
            }
        }


        private double? _AltitudeAccuracy;
        public double? AltitudeAccuracy
        {
            get { return this._AltitudeAccuracy; }
            set
            {
               
                    _AltitudeAccuracy = value;
                    NotifyPropertyChanged();
               
            }
        }


        private double? _Heading;
        public double? Heading
        {
            get { return this._Heading; }
            set
            {
                
                    _Heading = value;
                    NotifyPropertyChanged();
               
            }
        }



        private double? _Speed;
        public double? Speed
        {
            get { return this._Speed; }
            set
            {
               
                    _Speed = value;
                    NotifyPropertyChanged();
               
            }
        }
        
      

        public override string ToString()
        {

            return string.Format(CultureInfo.InvariantCulture, "{0:G}, {1:G}",
                Latitude,
                Longitude);
        }
    }

    public class GeoPosition:ModelBase
    {

        private CivicAdress _CivicAdress= new CivicAdress();
        public CivicAdress CivicAdress
        {
            get { return this._CivicAdress; }
            set
            {
               
                    _CivicAdress = value;
                    NotifyPropertyChanged();
               
            }
        }


        private Coordinate _Coordinate= new Coordinate();
        public Coordinate Coordinate
        {
            get { return this._Coordinate; }
            set
            {
               
                    _Coordinate = value;
                    NotifyPropertyChanged();
                
            }
        }

      



    }
}
