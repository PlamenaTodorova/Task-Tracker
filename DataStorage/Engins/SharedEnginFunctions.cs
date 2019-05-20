using Models.BindingModels;
using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStorage.Engins
{
    internal static class SharedEnginFunctions
    {
        public static int Add(TaskContext context, TaskBindingModel model)
        {
            if (model.TaskType == "Goal")
                return AddGoal(context, model);
            else if (model.TaskType == "Appointment")
                return AddAppointment(context, model);
            else
                return AddTask(context, model);
        }

        private static int AddTask(TaskContext context, TaskBindingModel model)
        {
            Task task = new Task()
            {
                Name = model.Name,
                Deadline = new DateTime(model.Deadline.Year, model.Deadline.Month, model.Deadline.Day, 23, 59, 59),
                Description = model.Description,
                Type = context.Type.FirstOrDefault(e => e.Name == model.TaskType)
            };

            context.Tasks.Add(task);
            context.SaveChanges();

            return task.Id;
        }

        private static int AddGoal(TaskContext context, TaskBindingModel model)
        {
            Goal goal = new Goal()
            {
                Name = model.Name,
                Span = model.Period,
                Description = model.Description,
            };

            goal.SetDate(DateTime.Today);

            context.Goals.Add(goal);
            context.SaveChanges();

            return goal.Id;
        }

        private static int AddAppointment(TaskContext context, TaskBindingModel model)
        {
            Appointment appointment = new Appointment()
            {
                Name = model.Name,
                Deadline = new DateTime(model.Deadline.Year, model.Deadline.Month, model.Deadline.Day, 23, 59, 59),
                Description = model.Description
            };

            
            context.Appointments.Add(appointment);
            context.SaveChanges();

            return appointment.Id;
        }
    }
}
