using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiPlatform.Domain.Code;

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
