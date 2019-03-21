namespace DataStorage.Migrations
{
    using Models.DatabaseModels;
    using System;
    using System.Collections.Generic;
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
            context.Type.AddOrUpdate(new TaskType()
            {
                    Name = "Task",
                    Id = 1,
                    PicturePath = "../Icons/task.png"
            });
            context.Type.AddOrUpdate(new TaskType()
            {
                Name = "Work",
                Id = 2,
                PicturePath = "../Icons/work.png"
            });
            context.Type.AddOrUpdate(new TaskType()
            {
                Name = "Social",
                Id = 3,
                PicturePath = "../Icons/social.png"
            });
            context.Type.AddOrUpdate(new TaskType()
            {
                Name = "Me",
                Id = 4,
                PicturePath = "../Icons/me.png"
            });
            context.Type.AddOrUpdate(new TaskType()
            {
                Name = "Apointment",
                Id = 5,
                PicturePath = "../Icons/apointment.png"
            });
            context.SaveChanges();
        }
    }
}
