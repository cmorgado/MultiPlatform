using GalaSoft.MvvmLight.Command;
using MultiPlatform.Domain.Code;
using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
  public  class Login :BaseViewModel
    {



      public Login(
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
            this.PageTitle = International.Translation.Login_Title;

        }

        private string _Email;
        public string Email
        {
            get { return this._Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _Password;
        public string Password
        {
            get { return this._Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private RelayCommand _DoLogin;
        public RelayCommand DoLogin
        {
            get
            {
                return _DoLogin ?? (_DoLogin = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _uxService.DoLogin();

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
