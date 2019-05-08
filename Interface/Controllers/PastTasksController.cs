using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage;
using Models.BindingModels;
using Models.ViewModels;
using Utilities;

namespace Interface.Controllers
{
    internal class PastTasksController
    {
        private ObservableCollection<HistoryViewModel> tasks;
        private ObservableCollection<HistoryViewModel> goals;
        private DateTime date;

        public PastTasksController(DateTime date) 
        {
            this.date = date;
            GenerateGoals();
            GenerateTasks();
        }

        public ObservableCollection<HistoryViewModel> GetTasks()
        {
            return this.tasks;
        }

        public ObservableCollection<HistoryViewModel> GetGoals()
        {
            return this.goals;
        }

        public void Check(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();

            HistoryViewModel model = data[1] == "Goal"
                ? goals.FirstOrDefault(e => e.Id == idAndType)
                : tasks.FirstOrDefault(e => e.Id == idAndType);

            if (model.IsFinishedPath == Constants.UnfinishedIcon)
            {
                Engin.GetEngin().GetTasksEngin().Check(int.Parse(data[0]), model);
                model.IsFinishedPath = Constants.FinishedIcon;

                if (data[1] == "Goal")
                {
                    HelperFunctions.RemoveElement<HistoryViewModel>(this.goals, model);
                    HelperFunctions.PutInTheRightPlace<HistoryViewModel>(this.goals, model);
                }
                else
                {
                    HelperFunctions.RemoveElement<HistoryViewModel>(this.tasks, model);
                    HelperFunctions.PutInTheRightPlace<HistoryViewModel>(this.tasks, model);
                }                
            }
        }

        private void GenerateTasks()
        {
            List<HistoryViewModel> tasks = Engin.GetEngin().GetTasksEngin().GetOldTasks(this.date).ToList();
            this.tasks = new ObservableCollection<HistoryViewModel>(tasks);
        }

        private void GenerateGoals()
        {
            List<HistoryViewModel> goals = Engin.GetEngin().GetTasksEngin().GetOldGoals(this.date).ToList();
            this.goals = new ObservableCollection<HistoryViewModel>(goals);
        }
    }
}
