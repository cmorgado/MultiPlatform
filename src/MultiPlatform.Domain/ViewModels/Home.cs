using GalaSoft.MvvmLight.Command;

using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
    public class Home : BaseViewModel
    {
      
        public readonly INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> _navigationService;
        public readonly IStorage _storageService;
        public readonly IUx _uxService;

        public Home(INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> navigationService, IStorage storageService
            , IUx uxService
            )
        {
            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.Home_Title;
            _navigationService = navigationService;
            _storageService = storageService;
            _uxService = uxService;
        
           

        }



        private bool _Refesh;
        public bool Refesh
        {
            get { return this.IsLogged; }
            set
            {
                if (_Refesh != value)
                {
                    _Refesh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelayCommand _GoToMap;
        public RelayCommand GoToMap
        {
            get
            {
                return _GoToMap ?? (_GoToMap = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _navigationService.Navigate<Map>(false);

                      }
                      catch (Exception ex)
                      {

                          throw ex;
                      }

                  }));
            }

        }

        
        private RelayCommand _GoDetails;
        public RelayCommand GoDetails
        {
            get
            {
                return _GoDetails ?? (_GoDetails = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _navigationService.Navigate<Details>(false);

                      }
                      catch (Exception ex)
                      {

                          throw ex;
                      }

                  }));
            }

        }


        private RelayCommand _GoToLogin;
        public RelayCommand GoToLogin
        {
            get
            {
                return _GoToLogin ?? (_GoToLogin = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _uxService.GoToLogin();

                      }
                      catch (Exception ex)
                      {

                          throw ex;
                      }

                  }));
            }

        }

        private RelayCommand _ShowAlert;
        public RelayCommand ShowAlert
        {
            get
            {
                return _ShowAlert ?? (_ShowAlert = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _uxService.ShowMessageBox("Conditional Compilation!");
                          _uxService.ShowToast("Conditional Compilation!");

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
