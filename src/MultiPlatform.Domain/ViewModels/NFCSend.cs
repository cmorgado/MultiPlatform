using GalaSoft.MvvmLight.Command;
using MultiPlatform.Domain.Code;
using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
    public class NFCSend : BaseViewModel
    {




        public NFCSend(
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

      

        private RelayCommand _StartPeering;
        public RelayCommand StartPeering
        {
            get
            {
                return _StartPeering ?? (_StartPeering = new RelayCommand(
                  async () =>
                  {

                      try
                      {

                          _peerConnectorService.StartConnect();


                      }
                      catch (Exception ex)
                      {

                          throw ex;
                      }

                  }));
            }

        }

        private RelayCommand _DoSend;
        public RelayCommand DoSend
        {
            get
            {
                return _DoSend ?? (_DoSend = new RelayCommand(
                  async () =>
                  {

                      try
                      {


                          string Message = "HelloWord";
                          byte[] toSend = Utils.GetBytes(Message);
                          _peerConnectorService.SendDataAsync(toSend);

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
