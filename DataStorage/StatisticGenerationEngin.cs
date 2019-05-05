using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public class StatisticGenerationEngin
    {
        private TaskContext context;

        internal StatisticGenerationEngin(TaskContext context)
        {
            this.context = context;
        }

        public ICollection<StatViewModel> GetStats()
        {
            List<StatViewModel> view = new List<StatViewModel>();

            foreach (Goal goal in context.Goals)
            {
                view.Add(this.GenerateView(goal));
            }

            return view;
        }

        private StatViewModel GenerateView(Goal goal)
        {
            StatViewModel model = new StatViewModel();
            model.Name = goal.Name;
            model.Period = goal.Span.ToString();
            model.Description = goal.Description;

            model.Successful = this.GetSuccessfulGoals(goal);
            model.Total = this.GetTotalTimesPassed(goal);

            return model;
        }

        private int GetTotalTimesPassed(Goal goal)
        {
            throw new NotImplementedException();
        }

        private int GetSuccessfulGoals(Goal goal)
        {
            return this.context.GoalsLog
                .Where(g => g.Goal.Id == goal.Id)
                .Count();
        }
    }
}
