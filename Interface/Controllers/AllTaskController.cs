using DataStorage;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Interface.Controllers
{
    internal class AllTaskController : ShowTaskController
    {
        public AllTaskController() 
            : base(DateTime.Today)
        {
        }

        protected override void GenerateTasks()
        {
            List<TaskViewModel> tasks = Engin.GetEngin().GetAll().ToList();
            this.tasks = new ObservableCollection<TaskViewModel>(tasks);
        }

        protected override void ReAddGoal(TaskViewModel model)
        {
            HelperFunctions.RemoveElement<TaskViewModel>(this.goals, model);
            this.goals.Add(model);
            this.tasks.Add(model);
        }
    }
}
