namespace DataStorage
{
    using Models.DatabaseModels;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TaskContext : DbContext
    {
        public TaskContext()
            : base("name=TaskContext")
        {
        }
        
        //Main
        public IDbSet<Task> Tasks { get; set; }
        public IDbSet<Goal> Goals { get; set; }

        //Additional
        public IDbSet<TaskType> Type { get; set; }

        internal void RecalculateGoalsDates()
        {
            foreach (Goal goal in Goals)
            {
                while (goal.Deadline < DateTime.Today)
                {
                    goal.RescheduleGoal();
                }
            }
        }
    }
}