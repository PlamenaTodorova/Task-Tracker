namespace DataStorage.Migrations
{
    using Models.DatabaseModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TaskContext context)
        {
            TaskType type = new TaskType();
            type.Name = "Task";
            type.PicturePath = "../Icons/task.png";
            context.Type.AddOrUpdate(type);
            context.SaveChanges();
        }
    }
}
