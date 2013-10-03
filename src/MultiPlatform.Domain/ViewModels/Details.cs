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
            this.PageTitle = International.Translation.Details_Title;

        }
    }
}
