using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Controllers
{
    internal abstract class ShowTaskController
    {
        protected ObservableCollection<TaskViewModel> tasks;
        private ObservableCollection<TaskViewModel> goals;

        public ShowTaskController()
        {
            GenerateTasks();
            GenerataGoals();
        }

        public ObservableCollection<TaskViewModel> GetTasks()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TaskViewModel> GetGoals()
        {
            throw new NotImplementedException();
        }

        public bool ChangeTask()
        {
            throw new NotImplementedException();
        }

        public bool DeleteTask()
        {
            throw new NotImplementedException();
        }

        protected abstract void GenerateTasks();

        private void GenerataGoals()
        {

        }
    }
}
