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
        protected ICollection<TypeBindingModel> model;

        public ShowTaskController(DateTime date, ICollection<TypeBindingModel> model) 
        {
            this.date = date;
            this.model = model;
            GenerateTasks();
            GenerataGoals();
        }

        public ShowTaskController(DateTime date)
            :this(date, null)
        {
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

        public void ChangeTask(int id, TaskBindingModel model)
        {
            
        }

        public void DeleteTask(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();
            int id = int.Parse(data[0]);
            string type = data[1];

            Engin.GetEngin().Delete(id, type);

            TaskViewModel model = data[1] == "Goal" ?
                goals.FirstOrDefault(e => e.Id == idAndType) : tasks.FirstOrDefault(e => e.Id == idAndType);

            HelperFunctions.RemoveElement<TaskViewModel>(goals, model);
            HelperFunctions.RemoveElement<TaskViewModel>(tasks, model);
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
