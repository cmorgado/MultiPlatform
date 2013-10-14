using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MultiPlatform.Domain.Interfaces
{
    public interface ISQLite
    {

        void Init();
        Task<bool> InsertTask(MultiPlatform.Domain.Models.Ui.Task task);
        Task<ObservableCollection<Models.Ui.Task>> GetAllTasks();
    }
}
