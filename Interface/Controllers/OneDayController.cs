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
    internal class OneDayController : ShowTaskController
    {
        public OneDayController(DateTime date)
            :base(date)
        {
        }

        protected override void GenerateTasks()
        {
            List<TaskViewModel> tasks = Engin.GetEngin().GetTasks(this.date).ToList();
            this.tasks = new ObservableCollection<TaskViewModel>(tasks);
        }

        protected override void RaAddModel(TaskViewModel changed)
        {
            if (changed.Type == "Goal")
                this.goals.Add(changed);
            else if (changed.Deadline <= date.AddDays(Constants.NumberOfDays))
                this.tasks.Add(changed);
        }

        protected override void ReAddGoal(TaskViewModel model)
        {
            HelperFunctions.RemoveElement<TaskViewModel>(this.goals, model);
        }
    }
}
