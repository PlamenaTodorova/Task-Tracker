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
        private TaskEngin task;

        private Engin()
        {
            this.context = new TaskContext();
            this.task = new TaskEngin(this.context);
            this.statistic = new StatisticGenerationEngin(this.context);
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

        public TaskEngin GetTasksEngin()
        {
            return this.task;
        }
    }
}
