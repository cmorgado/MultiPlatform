using GalaSoft.MvvmLight.Command;
using MultiPlatform.Domain.Code;
using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
    public class Details : BaseViewModel
    {



        public Details(
            INavigation<Domain.Interfaces.NavigationModes> navigationService
            , IStorage storageService
            , ISettings settingsService
            , IUx uxService
            , ILocation locationService
            , IPeerConnector peerConnectorService
            , INetwork networkService
            )
            : base(
            navigationService
            , storageService
            , settingsService
            , uxService
            , locationService
            , peerConnectorService
            , networkService
            )
        {

            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.Details_Title;

        }


        private RelayCommand _Load;
        public RelayCommand Load
        {
            get
            {
                return _Load ?? (_Load = new RelayCommand(
                  () =>
                  {

                      try
                      {
                          //   Domain.Code.Utils.ImageType imageType = (Domain.Code.Utils.ImageType)Enum.Parse(typeof(Domain.Code.Utils.ImageType), parameter.ToString());

                          LoadingCounter++;
                          _uxService.ShowMessageBox(this.navigationParameterJson);
                          LoadingCounter--;
                      }
                      catch (Exception ex)
                      {
                          LoadingCounter--;
                          throw ex;
                      }

                  }));
            }

        }
    }
}
