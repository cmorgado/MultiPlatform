using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
    public class BaseViewModel : Models.ModelBase
    {



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
