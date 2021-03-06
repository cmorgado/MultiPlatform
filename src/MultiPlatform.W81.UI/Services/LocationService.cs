﻿using MultiPlatform.Domain.Interfaces;

using MultiPlatform.Domain.Models.Ui.Map;
using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;



namespace MultiPlatform.W81.UI.Services
{
    public static class LocationExtensions
    {


        public static Windows.Devices.Geolocation.PositionAccuracy ToPositionAccuracy(this LocationAccuracy accuracy)
        {
            switch (accuracy)
            {
                case LocationAccuracy.Default:
                    return PositionAccuracy.Default;

                case LocationAccuracy.High:
                    return PositionAccuracy.High;

                default:
                    throw new ArgumentOutOfRangeException("accuracy");
            }
        }

        public static Domain.Models.Ui.Map.GeoPosition FromGeoposition(this Windows.Devices.Geolocation.Geoposition _gepos)
        {
            if (_gepos == null)
            {
                return default(Domain.Models.Ui.Map.GeoPosition);
            }

            var address = _gepos.CivicAddress;
            Domain.Models.Ui.Map.CivicAdress modelAdress = new CivicAdress();
            if (address != null)
            {
                modelAdress = new Domain.Models.Ui.Map.CivicAdress
                {
                    City = address.City,
                    Country = address.Country,
                    State = address.State
                    ,
                    PostalCode = address.PostalCode
                    ,
                    TimeStamp = address.Timestamp
                };
            }

            return new Domain.Models.Ui.Map.GeoPosition
            {
                CivicAdress = modelAdress
                    ,
                Coordinate = new Domain.Models.Ui.Map.Coordinate
                {
                    Accuracy = _gepos.Coordinate.Accuracy
                     ,
                    Altitude = _gepos.Coordinate.Altitude
                     ,
                    AltitudeAccuracy = _gepos.Coordinate.AltitudeAccuracy
                     ,
                    Heading = _gepos.Coordinate.Heading
                      ,
                    Latitude = _gepos.Coordinate.Latitude
                    ,
                    Longitude = _gepos.Coordinate.Longitude
                    ,
                    Speed = _gepos.Coordinate.Speed
                }
            };


        }


        public static Domain.Models.Ui.Map.Coordinate ToCoordinate(this Windows.Devices.Geolocation.Geocoordinate coordinate)
        {
            return new Domain.Models.Ui.Map.Coordinate
            {
                TimeStamp = coordinate.Timestamp,
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude,
                Accuracy = coordinate.Accuracy,
                Altitude = coordinate.Altitude,
                AltitudeAccuracy = coordinate.AltitudeAccuracy,
                Heading = coordinate.Heading,
                Speed = coordinate.Speed
            };
        }


        public static GeoPositionChangedEventArgs ToLocationServicePositionChangedEventArgs(this PositionChangedEventArgs eventArgs)
        {
            return new GeoPositionChangedEventArgs(eventArgs.Position.FromGeoposition());
        }

        public static LocationStatusChangedEventArgs ToLocationStatus(this StatusChangedEventArgs arg)
        {
            MultiPlatform.Domain.Interfaces.LocationStatus Status;
            switch (arg.Status)
            {
                case PositionStatus.Ready:
                    Status = LocationStatus.Ready;
                    break;
                case PositionStatus.Initializing:
                    Status = LocationStatus.Initializing;
                    break;
                case PositionStatus.NoData:
                    Status = LocationStatus.NoData;
                    break;
                case PositionStatus.Disabled:
                    Status = LocationStatus.Disabled;
                    break;
                case PositionStatus.NotInitialized:
                    Status = LocationStatus.NotInitialized;
                    break;
                case PositionStatus.NotAvailable:
                    Status = LocationStatus.NotAvailable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("positionStatus");
            }
            return new LocationStatusChangedEventArgs(Status);
        }
    }


    public class LocationService : Domain.Interfaces.ILocation
    {
        private readonly Geolocator _geolocator;

        public event EventHandler<GeoPositionChangedEventArgs> PositionChanged;
        public event EventHandler<LocationStatusChangedEventArgs> StatusChanged;

        public LocationService()
        {
            _geolocator = new Geolocator();
        }

        public void Start()
        {
            Start(LocationAccuracy.Default);
        }

        public void Start(LocationAccuracy desiredAccuracy)
        {
            _geolocator.DesiredAccuracy = desiredAccuracy.ToPositionAccuracy();
            _geolocator.StatusChanged += _geolocator_StatusChanged;
            _geolocator.PositionChanged += _geolocator_PositionChanged;

#if WINDOWS_PHONE
            _geolocator.DesiredAccuracyInMeters = null;
#endif

        }

        private void _geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var eventHandler = PositionChanged;

            if (eventHandler != null)
            {
                eventHandler(this, args.ToLocationServicePositionChangedEventArgs());
            }
        }


        private void _geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            var eventHandler = StatusChanged;

            if (eventHandler != null)
            {
                eventHandler(this, args.ToLocationStatus());
            }
        }

        public void Start(int desiredAccuracyInMeters)
        {

#if WINDOWS_PHONE
            _geolocator.DesiredAccuracyInMeters = null;
#endif



        }

        public void Stop()
        {
            _geolocator.PositionChanged -= _geolocator_PositionChanged;
            _geolocator.StatusChanged -= _geolocator_StatusChanged;
        }

        public async Task<Domain.Models.Ui.Map.GeoPosition> GetPosition(LocationAccuracy desiredAccuracy)
        {
            try
            {
                _geolocator.DesiredAccuracy = desiredAccuracy.ToPositionAccuracy();
                var g = await _geolocator.GetGeopositionAsync();
                return g.FromGeoposition();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Domain.Models.Ui.Map.GeoPosition> GetPositionAsync(LocationAccuracy desiredAccuracy, TimeSpan maximumAge, TimeSpan timeout)
        {
            try
            {
                _geolocator.DesiredAccuracy = desiredAccuracy.ToPositionAccuracy();
                var g = await _geolocator.GetGeopositionAsync(maximumAge, timeout);
                return g.FromGeoposition();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




    }
}