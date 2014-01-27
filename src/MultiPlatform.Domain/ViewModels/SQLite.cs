using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MultiPlatform.Domain.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace MultiPlatform.Domain.ViewModels
{
    public class SQLite : BaseViewModel
    {
        public readonly ISQLite _sqliteService;
        public SQLite(
            INavigation<Domain.Interfaces.NavigationModes> navigationService
            , IStorage storageService
            , ISettings settingsService
            , IUx uxService
            , ILocation locationService
            , IPeerConnector peerConnectorService
            , ISQLite sqliteService
            , INetwork networkService
            )
            : base(
            navigationService
            , storageService
            , settingsService
            , uxService
            , locationService
            , peerConnectorService
            , networkService
            )
        {

            this.AppName = International.Translation.AppName;
            this.PageTitle = International.Translation.SQLite_Title;
            _sqliteService = sqliteService;
        }


        private Models.Ui.Task _NewTask= new Models.Ui.Task();
        public Models.Ui.Task NewTask
        {
            get { return this._NewTask; }
            set
            {
                if (_NewTask != value)
                {
                    _NewTask = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ObservableCollection<Models.Ui.Task> _Items;
        public ObservableCollection<Models.Ui.Task> Items
        {
            get { return this._Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelayCommand _AddTask;
        public RelayCommand AddTask
        {
            get
            {
                return _AddTask ?? (_AddTask = new RelayCommand(
                async () =>
                {

                    try
                    {
                        LoadingCounter++;
                        this.NewTask.IdTask = Guid.NewGuid().ToString();
                        await _sqliteService.InsertTask(this.NewTask);
                        this.Items = await _sqliteService.GetAllTasks();
                        LoadingCounter--;

                    }
                    catch (Exception ex)
                    {
                        LoadingCounter--;
                        throw ex;
                    }

                }));
            }

        }

        private RelayCommand _LoadTasks;
        public RelayCommand LoadTasks
        {
            get
            {
                return _LoadTasks ?? (_LoadTasks = new RelayCommand(
                  async () =>
                  {

                      try
                      {
                          LoadingCounter++;
                          this.Items = await _sqliteService.GetAllTasks();
                          LoadingCounter--;

                      }
                      catch (Exception ex)
                      {
                          LoadingCounter--;
                          throw ex;
                      }

                  }));
            }

        }
    }
}
