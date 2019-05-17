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
    internal class TomorrowsTaskController 
    {
        private ObservableCollection<TomorrowViewModel> tasks;
        private ObservableCollection<TomorrowViewModel> goals;
        private DateTime date;

        public TomorrowsTaskController(DateTime date)
        {
            this.date = date;
            this.GenerateGoals();
            this.GenerateTasks();
        }

        public ObservableCollection<TomorrowViewModel> GetTasks()
        {
            return this.tasks;
        }

        public ObservableCollection<TomorrowViewModel> GetGoals()
        {
            return this.goals;
        }

        public TaskBindingModel GetBinding(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();
            return Engin.GetEngin().GetTasksEngin().GetTask(int.Parse(data[0]), data[1]);
        }

        public void Check(string id)
        {
            string[] data = id.Split(':').ToArray();
            TomorrowViewModel model = PickedTask(id);

            model.Deadline = Engin.GetEngin().GetTasksEngin().Check(int.Parse(data[0]), model);

            if (data[1] == "Goal")
                ReAddGoal(model);
            else
                HelperFunctions.RemoveElement<TomorrowViewModel>(tasks, model);
        }

        public void ChangeTask(string idAndType, TaskBindingModel model)
        {
            this.RemoveModel(idAndType);

            string[] data = idAndType.Split(':').ToArray();
            TaskViewModel result = Engin.GetEngin().GetTasksEngin().Change(int.Parse(data[0]), data[1], model, date);

            TomorrowViewModel changed = new TomorrowViewModel(result, true);

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
            TomorrowViewModel model = PickedTask(idAndType);

            HelperFunctions.RemoveElement<TomorrowViewModel>(goals, model);
            HelperFunctions.RemoveElement<TomorrowViewModel>(tasks, model);
        }

        private TomorrowViewModel PickedTask(string idAndType)
        {
            string[] data = idAndType.Split(':').ToArray();
            return data[1] == "Goal"
                ? goals.FirstOrDefault(e => e.Id == idAndType)
                : tasks.FirstOrDefault(e => e.Id == idAndType);
        }

        protected void RaAddModel(TomorrowViewModel changed)
        {
            if (changed.Type == "Goal")
            {
                if (changed != null)
                    HelperFunctions.PutInTheRightPlace<TomorrowViewModel>(this.goals, changed);
            }
            else if (changed.Deadline <= date.AddDays(Constants.NumberOfDays))
                HelperFunctions.PutInTheRightPlace<TomorrowViewModel>(this.tasks, changed);
        }

        protected void ReAddGoal(TomorrowViewModel model)
        {
            HelperFunctions.RemoveElement<TomorrowViewModel>(this.goals, model);
        }

        private void GenerateTasks()
        {
            List<TomorrowViewModel> tasks = Engin.GetEngin().GetTasksEngin().GetFutureTask(this.date).ToList();
            this.tasks = new ObservableCollection<TomorrowViewModel>(tasks);
        }

        private void GenerateGoals()
        {
            List<TomorrowViewModel> goals = Engin.GetEngin().GetTasksEngin().GetFutureGoal(this.date).ToList();
            this.goals = new ObservableCollection<TomorrowViewModel>(goals);
        }
    }
}
