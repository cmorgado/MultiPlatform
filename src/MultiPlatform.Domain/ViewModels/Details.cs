using MultiPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.ViewModels
{
    public class Details:BaseViewModel
    {
         public readonly INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> _navigationService;
         public readonly IStorage _storageService;


         public Details(INavigation<MultiPlatform.Domain.Interfaces.NavigationModes> navigationService, IStorage storageService)
        {
            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.Details_Title;
            _navigationService = navigationService;
            _storageService = storageService;
        }
    }
}
