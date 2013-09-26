using GalaSoft.MvvmLight.Command;
using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
  public  class Login :BaseViewModel
    {

        public readonly INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> _navigationService;
        public readonly IStorage _storageService;
        public readonly IUx _uxService;

        public Login(INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> navigationService, IStorage storageService, IUx uxService)
        {
            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.Login_Title;
            _navigationService = navigationService;
            _storageService = storageService;
            _uxService = uxService;
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
