using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.BindingModels;
using Models.DatabaseModels;
using Models.ViewModels;
using Utilities;

namespace DataStorage.Engins
{
    internal class CurrentTaskEngin : BaseTaskEngin
    {
        public CurrentTaskEngin(TaskContext context) 
            : base(context)
        {
        }

        public override ICollection<TaskViewModel> GetGoals(DateTime date)
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach (Goal goal in this.Context.Goals)
            {
                if (this.CurrentlyUnfished(goal, date))
                {
                    views.Add(this.GenerateView(goal, date));
                }
            }

            views.Sort();
            return views;
        }

        public override ICollection<TaskViewModel> GetTasks(DateTime date)
        {
            DateTime deadline = date.AddDays(Constants.NumberOfDays);
            List<TaskViewModel> views = new List<TaskViewModel>();
            List<Task> currentTasks = this.Context.Tasks
                .Where(t => t.Deadline <= deadline && t.Deadline >= date && !t.IsFinished)
                .ToList();

            foreach (Task task in currentTasks)
            {
                views.Add(this.GenerateView(task));
            }

            views.Sort();
            return views;
        }

        public override ICollection<TaskViewModel> GetAll(ICollection<TypeBindingModel> chosen)
        {
            List<int> indexes = chosen
                .Where(e => e.Chosen)
                .Select(e => e.Id)
                .ToList();

            List<Task> tasks = this.Context.Tasks
                .Where(e => indexes.Contains(e.Type.Id) && e.IsFinished == false)
                .ToList();

            List<TaskViewModel> views = this.GetAll(tasks);

            if (indexes.Contains(-1))
            {
                views.AddRange(this.GetGoals(DateTime.Today));
            }

            views.Sort();
            return views;
        }

        public override ICollection<TypeBindingModel> GetAllTypes()
        {
            List<TypeBindingModel> bind = new List<TypeBindingModel>();

            foreach (TaskType type in this.Context.Type)
            {
                bind.Add(new TypeBindingModel(type.Name, type.Id));
            }

            bind.Add(new TypeBindingModel("Goal", -1));

            return bind;
        }

        private List<TaskViewModel> GetAll(List<Task> currentTasks)
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach (Task task in currentTasks)
            {
                TaskViewModel view = this.GenerateView(task);

                views.Add(view);
            }

            return views;
        }
    }
}
