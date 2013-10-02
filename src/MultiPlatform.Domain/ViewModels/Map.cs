using GalaSoft.MvvmLight.Command;
using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
    public class Map : BaseViewModel
    {
        public readonly INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> _navigationService;
        public readonly IUx _uxService;
        public readonly ILocation _locationService;

        public Map(INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> navigationService, ILocation locationService, IUx uxService)
        {
            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.Map_Title;
            _navigationService = navigationService;
            _locationService = locationService;
            _uxService = uxService;



        }


        private Models.Ui.GeoPosition _CurrentPosition = new Models.Ui.GeoPosition();
        public Models.Ui.GeoPosition CurrentPosition
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
                         CurrentPosition = new Models.Ui.GeoPosition
                         {
                             Coordinate = new Models.Ui.Coordinate
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
