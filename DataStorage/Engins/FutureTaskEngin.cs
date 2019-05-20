using System;
using System.Collections.Generic;
using System.Linq;
using Models.DatabaseModels;
using Models.ViewModels;
using Utilities;

namespace DataStorage.Engins
{
    internal class FutureTaskEngin : BaseTaskEngin
    {
        public FutureTaskEngin(TaskContext context) 
            : base(context)
        {
        }

        public override ICollection<TaskViewModel> GetGoals(DateTime date)
        {
            List<TomorrowViewModel> views = new List<TomorrowViewModel>();
            List<Goal> suitable = this.Context.Goals.ToList();

            foreach (Goal goal in suitable)
            {
                TaskViewModel baseModel = this.GenerateView(goal, date);
                bool isFinished = IsGoalFinished(goal, baseModel.Deadline);
                TomorrowViewModel model =
                    new TomorrowViewModel(baseModel, isFinished);

                views.Add(model);
            }

            views.Sort();
            return views.Select(e => e as TaskViewModel).ToList();
        }

        public override ICollection<TaskViewModel> GetTasks(DateTime date)
        {
            DateTime deadline = date.AddDays(Constants.NumberOfDays);
            List<TomorrowViewModel> views = new List<TomorrowViewModel>();
            List<Task> currentTasks = this.Context.Tasks
                .Where(t => t.Deadline <= deadline && t.Deadline >= date && !t.IsFinished)
                .ToList();

            foreach (Task task in currentTasks)
            {
                TomorrowViewModel model = new TomorrowViewModel(this.GenerateView(task), true);
                views.Add(model);
            }

            List<Appointment> currentAppointment = this.Context.Appointments
                .Where(t => t.Deadline <= deadline && t.Deadline >= date)
                .ToList();

            foreach (Appointment task in currentAppointment)
            {
                TomorrowViewModel model = new TomorrowViewModel(this.GenerateView(task), true);
                views.Add(model);
            }

            views.Sort();
            return views.Select(e => e as TaskViewModel).ToList(); ;
        }
    }
}
