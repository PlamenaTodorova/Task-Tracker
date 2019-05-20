using DataStorage.Engins;
using Models.BindingModels;
using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace DataStorage
{
    public class Engin
    {
        private TaskContext context;
        private static Engin engin;
        private StatisticGenerationEngin statistic;
        
        private CurrentTaskEngin current;
        private FutureTaskEngin future;
        private PastTaskEngin past;
        private BaseTaskEngin inUse;

        private Engin()
        {
            this.context = new TaskContext();

            this.context.RecalculateGoalsDates();
            this.context.SaveChanges();

            this.statistic = new StatisticGenerationEngin(this.context);

            this.current = new CurrentTaskEngin(this.context);
            this.future = new FutureTaskEngin(this.context);
            this.past = new PastTaskEngin(this.context);
        }

        public void ChangeContext(DateTime date)
        {
            if (date < DateTime.Today)
                this.inUse = this.past;
            else if (date == DateTime.Today)
                this.inUse = this.current;
            else
                this.inUse = this.future;
        }

        public BaseTaskEngin GetTasksEngin()
        {
            return this.inUse;
        }

        public static Engin GetEngin()
        {
            if (engin == null)
                engin = new Engin();

            return engin;
        }

        public StatisticGenerationEngin GetStatistic()
        {
            return this.statistic;
        }

        public ICollection<string> GetTypes()
        {
            List<string> types = context.Type
                .Select(e => e.Name)
                .ToList();
            types.Add("Appointment");
            types.Add("Goal");
            return types;
        }

        public int Add(TaskBindingModel model)
        {
            return SharedEnginFunctions.Add(this.context, model);
        }
    }
}
