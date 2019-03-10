using DataStorage;
using Models.BindingModels;
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
            return tasks;
        }

        public ObservableCollection<TaskViewModel> GetGoals()
        {
            return goals;
        }

        public bool ChangeTask(string id, TaskBindingModel model)
        {
            return Engin.GetEngin().Change(id, model);
        }

        public bool DeleteTask(string id, TaskBindingModel model)
        {
            return Engin.GetEngin().Change(id, model);
        }

        protected abstract void GenerateTasks();

        private void GenerataGoals()
        {
            List<TaskViewModel> goals = Engin.GetEngin().GetGoals().ToList();
            this.goals = new ObservableCollection<TaskViewModel>(goals);
        }
    }
}
