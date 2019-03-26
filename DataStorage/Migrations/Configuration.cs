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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TaskContext context)
        {
            context.Type.AddOrUpdate(e => e.Name, 
            new TaskType()
            {
                    Name = "Task",
                    Id = 1,
                    PicturePath = "../Icons/task.png"
            },
            new TaskType()
            {
                Name = "Work",
                Id = 2,
                PicturePath = "../Icons/work.png"
            },
            new TaskType()
            {
                Name = "Social",
                Id = 3,
                PicturePath = "../Icons/social.png"
            },
            new TaskType()
            {
                Name = "Me",
                Id = 4,
                PicturePath = "../Icons/me.png"
            },
            new TaskType()
            {
                Name = "Apointment",
                Id = 5,
                PicturePath = "../Icons/apointment.png"
            });
        }
    }
}
