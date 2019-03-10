using DataStorage;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Controllers
{
    internal class OneDayController : ShowTaskController
    {
        private DateTime date;

        public OneDayController(DateTime date)
        {
            this.date = date;
        }

        protected override void GenerateTasks()
        {
            List<TaskViewModel> tasks = Engin.GetEngin().GetTasks(date).ToList();
            this.tasks = new ObservableCollection<TaskViewModel>(tasks);
        }
    }
}
