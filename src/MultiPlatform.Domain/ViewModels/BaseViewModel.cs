using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiPlatform.Domain.Code;
using GalaSoft.MvvmLight;
using MultiPlatform.Domain.Interfaces;
using GalaSoft.MvvmLight.Command;


namespace MultiPlatform.Domain.ViewModels
{
    public class BaseViewModel : Models.ModelBase
    {

        public readonly INavigation<Domain.Interfaces.NavigationModes> _navigationService;
        public Interfaces.NavigationModes NavigationType { get; set; }
        public readonly IStorage _storageService;
        public readonly ISettings _settingsService;
        public readonly IUx _uxService;
        public readonly ILocation _locationService;
        public readonly IPeerConnector _peerConnectorService;
        public readonly INetwork _networkService;

        public event EventHandler<ConnectionStatusChangedEventArgs> ConnectionStatusChanged;
        public event EventHandler<DataReceivedEventArgs> DataReceived;







        #region VM Props

        public string navigationParameterJson;

        public T NavigationParameter<T>()
        {
            return navigationParameterJson.FromJson<T>();
        }


        private string _AppName;
        public string AppName
        {
            get { return this._AppName; }
            set
            {
                if (_AppName != value)
                {
                    _AppName = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private int loadingCounter = 0;
        public int LoadingCounter
        {
            get { return loadingCounter; }
            set
            {
                loadingCounter = value;
                if (value != loadingCounter)
                {
                    loadingCounter = value;
                    // NotifyPropertyChanged();
                }
                if (loadingCounter < 0)
                    loadingCounter = 0;

                if (loadingCounter > 0)
                {
                    IsLoading = true;
                }
                else
                {
                    IsLoading = false;
                }
            }
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (value != isLoading)
                {
                    isLoading = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _IsLogged = false;
        public bool IsLogged
        {
            get { return this._IsLogged; }
            set
            {
                if (_IsLogged != value)
                {
                    _IsLogged = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private bool _IsConnected = false;
        public bool IsConnected
        {
            get { return this._IsConnected; }
            set
            {
                if (_IsConnected != value)
                {
                    _IsConnected = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _PageTitle;
        public string PageTitle
        {
            get { return this._PageTitle; }
            set
            {
                if (_PageTitle != value)
                {
                    _PageTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _CanGoBack = false;
        public bool CanGoBack
        {
            get { return _CanGoBack; }
            set
            {
                _CanGoBack = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Common Commands


        private RelayCommand _GoHome;
        public RelayCommand GoHome
        {
            get
            {
                return _GoHome ?? (_GoHome = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _navigationService.Navigate<Home>(false);

                      }
                      catch (Exception ex)
                      {
                          LoadingCounter--;
                          throw ex;
                      }

                  }));
            }

        }

        private RelayCommand _GoBack;
        public RelayCommand GoBack
        {
            get
            {
                return _GoBack ?? (_GoBack = new RelayCommand(
                  () =>
                  {

                      try
                      {

                          _navigationService.GoBack();

                      }
                      catch (Exception ex)
                      {
                          LoadingCounter--;
                          throw ex;
                      }

                  }));
            }

        }

        #endregion


        public BaseViewModel(
            INavigation<Domain.Interfaces.NavigationModes> navigationService
            , IStorage storageService
            , ISettings settingsService
            , IUx uxService
            , ILocation locationService
            , IPeerConnector peerConnectorService
            , INetwork networkService
           )
        {
            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;
            _locationService = locationService;
            _peerConnectorService = peerConnectorService;
             _networkService = networkService;


            _peerConnectorService.ConnectionStatusChanged += _peerConnectorService_ConnectionStatusChanged;
            _peerConnectorService.DataReceived += _peerConnectorService_DataReceived;
        }

        void _peerConnectorService_DataReceived(object sender, DataReceivedEventArgs e)
        {



            _uxService.ShowToast(Utils.GetString(e.Bytes));

            if (DataReceived != null)
            {
                DataReceived(this, new DataReceivedEventArgs());

            }
        }

        void _peerConnectorService_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case ConnectionStatus.Completed:
                    IsConnected = true;
                    break;
                case ConnectionStatus.Canceled:
                    // If I am already connected, the canel just means we accidentally tapped again and I want to stay connected.
                    // If I am not already connected, IsConnected is already false, so no need to do anything.
                    break;
                case ConnectionStatus.Disconnected:
                case ConnectionStatus.Failed:
                case ConnectionStatus.TapNotSupported:
                    IsConnected = false;
                    break;
            }

            _uxService.ShowToast(e.Status.ToString());

            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, new ConnectionStatusChangedEventArgs() { Status = e.Status });

        }

        #region Internet Connection

        void _networkService_InternetConnectionChanged(object sender, InternetConnectionChangedEventArgs e)
        {
            this.IsConnected = e.IsConnected;
        }
        #endregion

    }
}
