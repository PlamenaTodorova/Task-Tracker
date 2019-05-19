using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace DataStorage.Engins
{
    internal class PastTaskEngin : BaseTaskEngin
    {
        public PastTaskEngin(TaskContext context) 
            : base(context)
        {
        }

        public override ICollection<TaskViewModel> GetTasks(DateTime date)
        {
            List<HistoryViewModel> views = new List<HistoryViewModel>();
            List<Task> currentTasks = this.Context.Tasks
                .Where(t => t.Deadline.Year == date.Year && t.Deadline.Month == date.Month && t.Deadline.Day == date.Day)
                .ToList();

            foreach (Task task in currentTasks)
            {
                HistoryViewModel model = new HistoryViewModel(this.GenerateView(task));
                model.IsFinishedPath = task.IsFinished ? Constants.FinishedIcon : Constants.UnfinishedIcon;
                views.Add(model);
            }

            views.Sort();
            return views.Select(e => e as TaskViewModel).ToList();
        }

        public override ICollection<TaskViewModel> GetGoals(DateTime date)
        {
            List<HistoryViewModel> views = new List<HistoryViewModel>();
            List<Goal> suitable = this.Context.Goals.Where(g => g.OriginalDate <= date).ToList();

            foreach (Goal goal in suitable)
            {
                HistoryViewModel model = new HistoryViewModel(this.GenerateView(goal, date));

                model.Deadline = goal.SetBack(model.Deadline, date);
                DateTime beginPeriod = goal.LastPeriod(model.Deadline);
                bool flag = this.Context.GoalsLog
                    .Where(l => l.Date > beginPeriod && l.Date <= model.Deadline && l.Goal.Id == goal.Id)
                    .Count() != 0;
                model.IsFinishedPath = flag ? Constants.FinishedIcon : Constants.UnfinishedIcon;
                views.Add(model);
            }

            views.Sort();
            return views.Select(e => e as TaskViewModel).ToList();
        }

        protected override void CheckGoal(int id, TaskViewModel model)
        {
            Goal goal = this.Context.Goals.Find(id);

            this.AddToLog(goal, model.Deadline);

            model.Deadline = goal.Deadline;

            this.Context.SaveChanges();
        }
    }
}
