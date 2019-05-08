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

        public TaskBindingModel GetBinding(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();
            return Engin.GetEngin().GetTasksEngin().GetTask(int.Parse(data[0]), data[1]);
        }

        public void Check(string id)
        {
            string[] data = id.Split(':').ToArray();
            TaskViewModel model = PickedTask(id);

            model.Deadline = Engin.GetEngin().GetTasksEngin().Check(int.Parse(data[0]), model);
            
            if (data[1] == "Goal")
                ReAddGoal(model);
            else
                HelperFunctions.RemoveElement<TaskViewModel>(tasks, model);
        }

        public void ChangeTask(string idAndType, TaskBindingModel model)
        {
            this.RemoveModel(idAndType);

            string[] data = idAndType.Split(':').ToArray();
            TaskViewModel changed = Engin.GetEngin().GetTasksEngin().Change(int.Parse(data[0]), data[1], model);

            this.RaAddModel(changed);
        }

        public void DeleteTask(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();
            int id = int.Parse(data[0]);
            string type = data[1];

            Engin.GetEngin().GetTasksEngin().Delete(id, type);

            this.RemoveModel(idAndType);
        }

        private void RemoveModel(string idAndType)
        {
            TaskViewModel model = PickedTask(idAndType);

            HelperFunctions.RemoveElement<TaskViewModel>(goals, model);
            HelperFunctions.RemoveElement<TaskViewModel>(tasks, model);
        }

        protected abstract void GenerateTasks();

        protected abstract void ReAddGoal(TaskViewModel model);

        protected abstract void RaAddModel(TaskViewModel changed);

        private TaskViewModel PickedTask(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();
            return data[1] == "Goal"
                ? goals.FirstOrDefault(e => e.Id == idAndType)
                : tasks.FirstOrDefault(e => e.Id == idAndType);
        }

        private void GenerataGoals()
        {
            List<TaskViewModel> goals = Engin.GetEngin().GetTasksEngin().GetGoals(this.date).ToList();
            this.goals = new ObservableCollection<TaskViewModel>(goals);
        }
    }
}
