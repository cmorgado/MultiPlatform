using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Collections.ObjectModel;

namespace MultiPlatform.Shared.SQLite
{
    public class ServiceSQLite : Domain.Interfaces.ISQLite
    {
        public static SQLiteAsyncConnection Connection { get; set; }
        private string dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "data.db3");


        public ServiceSQLite()
        {
            Init();
        }

        private async void Create()
        {
            Connection = new SQLiteAsyncConnection(dbpath);

            // create all tables
            await Connection.CreateTableAsync<SQLite.Task>();
        }


        public async void Init()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(dbpath);
                Connection = new SQLiteAsyncConnection(dbpath);

            }
            catch (FileNotFoundException)
            {
                Create();
            }
        }

        public async Task<bool> InsertTask(Domain.Models.Ui.Task task)
        {

            try
            {

                SQLite.Task NewTask = new Task
                {
                    IdTask = task.IdTask,
                    Name = task.Name
                };
                await Connection.InsertAsync(NewTask);


                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ObservableCollection<Domain.Models.Ui.Task>> GetAllTasks()
        {
            var task = await Connection.Table<SQLite.Task>().ToListAsync();
            ObservableCollection<Domain.Models.Ui.Task> t = new ObservableCollection<Domain.Models.Ui.Task>();
            foreach (var item in task)
            {
                t.Add(new Domain.Models.Ui.Task
                {
                    Name = item.Name,
                    IdTask = item.IdTask
                });
            }

            return t;
        }

    }
}
