using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage;
using Models.BindingModels;
using Models.ViewModels;

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

        private void GenerateTasks()
        {
            List<HistoryViewModel> tasks = Engin.GetEngin().GetOldTasks(this.date).ToList();
            this.tasks = new ObservableCollection<HistoryViewModel>(tasks);
        }

        private void GenerateGoals()
        {
            List<HistoryViewModel> goals = Engin.GetEngin().GetOldGoals(this.date).ToList();
            this.goals = new ObservableCollection<HistoryViewModel>(goals);
        }
    }
}
