using DataStorage;
using Models.BindingModels;
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
    internal abstract class ShowTaskController
    {
        protected ObservableCollection<TaskViewModel> tasks;
        protected ObservableCollection<TaskViewModel> goals;
        protected DateTime date;

        public ShowTaskController(DateTime date)
        {
            this.date = date;
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

        public void Check(string id)
        {
            string[] data = id.Split(':').ToArray();
            TaskViewModel model = data[1] == "Goal" ?
                goals.FirstOrDefault(e => e.Id == id) : tasks.FirstOrDefault(e => e.Id == id);

            model.Deadline = Engin.GetEngin().Check(int.Parse(data[0]), model);
            
            if (data[1] == "Goal")
                ReAddGoal(model);
            else
                HelperFunctions.RemoveElement<TaskViewModel>(tasks, model);
        }

        public bool ChangeTask(int id, TaskBindingModel model)
        {
            return Engin.GetEngin().Change(id, model);
        }

        public bool DeleteTask(int id, TaskBindingModel model)
        {
            return Engin.GetEngin().Change(id, model);
        }

        protected abstract void GenerateTasks();

        protected abstract void ReAddGoal(TaskViewModel model);

        private void GenerataGoals()
        {
            List<TaskViewModel> goals = Engin.GetEngin().GetGoals(this.date).ToList();
            this.goals = new ObservableCollection<TaskViewModel>(goals);
        }
    }
}
