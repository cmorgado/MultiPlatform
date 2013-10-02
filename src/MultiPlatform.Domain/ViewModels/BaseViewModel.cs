using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiPlatform.Domain.Code;
using GalaSoft.MvvmLight;


namespace MultiPlatform.Domain.ViewModels
{
    public class BaseViewModel : Models.ModelBase
    {
      

        public string navigationParameterJson;
        public T NavigationParameter<T>()
        {
            return navigationParameterJson.FromJson<T>();
        }


        public Interfaces.NavigationModes NavigationType { get; set; }


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

    }
}
