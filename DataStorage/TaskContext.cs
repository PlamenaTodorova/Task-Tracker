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
        public IDbSet<Task> Finished { get; set; }
    }
}