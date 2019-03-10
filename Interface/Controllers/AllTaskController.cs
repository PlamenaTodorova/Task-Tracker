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
    internal class AllTaskController : ShowTaskController
    {
        protected override void GenerateTasks()
        {
            List<TaskViewModel> tasks = Engin.GetEngin().GetAll().ToList();
            this.tasks = new ObservableCollection<TaskViewModel>(tasks);
        }
    }
}
