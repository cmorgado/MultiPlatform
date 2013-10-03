using GalaSoft.MvvmLight.Command;
using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MultiPlatform.Domain.Code;

namespace MultiPlatform.Domain.ViewModels
{
    public class Map : BaseViewModel
    {



        public Map(
            INavigation<Domain.Interfaces.NavigationModes> navigationService
            , IStorage storageService
            , ISettings settingsService
            , IUx uxService
            , ILocation locationService
            , IPeerConnector peerConnectorService
            )
            : base(
            navigationService
            , storageService
            , settingsService
            , uxService
            , locationService
                , peerConnectorService
            )
        {

            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.Map_Title;

        }



        private ObservableCollection<Models.Ui.Map.PushpinModel> _Items= new ObservableCollection<Models.Ui.Map.PushpinModel>();
        public ObservableCollection<Models.Ui.Map.PushpinModel> Items
        {
            get { return this._Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                    NotifyPropertyChanged();
                }
            }
        }
        

        private Models.Ui.Map.GeoPosition _CurrentPosition = new Models.Ui.Map.GeoPosition();
        public Models.Ui.Map.GeoPosition CurrentPosition
        {
            get { return this._CurrentPosition; }
            set
            {
              
                    _CurrentPosition = value;
                    NotifyPropertyChanged();
               
            }
        }


        private double _ZoomLevel = 1;
        public double ZoomLevel
        {
            get { return this._ZoomLevel; }
            set
            {
               
                    _ZoomLevel = value;
                    NotifyPropertyChanged();
               
            }
        }



        private RelayCommand _Load;
        public RelayCommand Load
        {
            get
            {
                return _Load ?? (_Load = new RelayCommand(
                 async () =>
                 {

                     try
                     {
                         LoadingCounter++;
                         ZoomLevel = 1;
                         var result = await _locationService.GetPosition(LocationAccuracy.Default);  
                         ZoomLevel = 13;
                         CurrentPosition = new Models.Ui.Map.GeoPosition
                         {
                             Coordinate = new Models.Ui.Map.Coordinate
                             {
                                 Longitude = result.Coordinate.Longitude,
                                 Latitude = result.Coordinate.Latitude
                             }
                         };
                       

                       
                         LoadingCounter--;
                         _uxService.ShowToast(CurrentPosition.Coordinate.ToString());

                     }
                     catch (Exception ex)
                     {

                         throw ex;
                     }

                 }));
            }

        }
    }
}
