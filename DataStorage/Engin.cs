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

        private Engin()
        {
            this.context = new TaskContext();
        }

        public static Engin GetEngin()
        {
            if (engin == null)
                engin = new Engin();

            return engin;
        }

        public ICollection<TaskViewModel> GetTasks(DateTime date)
        {
            DateTime deadline = date.AddDays(Constants.NumberOfDays);
            List<TaskViewModel> views = new List<TaskViewModel>();
            List<Task> currentTasks = context.Tasks
                .Where(t => t.Deadline <= deadline)
                .ToList();

            foreach (Task task in currentTasks)
            {
                TaskViewModel view = new TaskViewModel();

                view.Name = task.Name;
                view.Deadline = task.Deadline;
                view.Description = task.Description;
                view.Type = task.Type.Name;
                view.PicturePath = task.Type.PicturePath;

                views.Add(view);
            }

            return views;
        }

        public ICollection<TaskViewModel> GetGoals(DateTime date)
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach(Goal goal in context.Goals)
            {
                TaskViewModel view = new TaskViewModel();

                view.Name = goal.Name;
                view.Deadline = goal.RecalculateDate(date);
                view.Description = goal.Description;
                view.Type = "Goal";
                view.PicturePath = Constants.GoalsIcon;

                views.Add(view);
            }

            return views;
        }

        public ICollection<TaskViewModel> GetAll()
        {
            List<TaskViewModel> views = new List<TaskViewModel>();
            List<Task> currentTasks = context.Tasks.ToList();

            foreach (Task task in currentTasks)
            {
                TaskViewModel view = new TaskViewModel();

                view.Name = task.Name;
                view.Deadline = task.Deadline;
                view.Description = task.Description;
                view.Type = task.Type.Name;
                view.PicturePath = task.Type.PicturePath;

                views.Add(view);
            }

            views.AddRange(this.GetGoals(DateTime.Today));

            return views;
        }

        public ICollection<TaskViewModel> GetAll(object[] filters)
        {
            throw new NotImplementedException();
            //TODO
        }

        public ICollection<string> GetTypes()
        {
            List<string> types = context.Type
                .Select(e => e.Name)
                .ToList();
            types.Add("Goal");
            return types;
        }

        public TaskBindingModel GetTask(int id)
        {
            throw new NotImplementedException();
            //TODO
        }

        public void Add(TaskBindingModel model)
        {
            if (model.TaskType == "Goal")
                this.AddGoal(model);
            else this.AddTask(model);
        }

        public bool Change(int id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Delete(int id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        private void AddTask(TaskBindingModel model)
        {
            Task task = new Task()
            {
                Name = model.Name,
                Deadline = model.Deadline,
                Description = model.Description,
                Type = context.Type.FirstOrDefault(e => e.Name == model.TaskType)
            };

            context.Tasks.Add(task);
            context.SaveChanges();
        }

        private void AddGoal(TaskBindingModel model)
        {
            Goal goal = new Goal()
            {
                Name = model.Name,
                Span = model.Period,
                Description = model.Description,
            };

            DateTime tomorrow = DateTime.Today.AddDays(1);
            goal.Deadline = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0);

            goal.RescheduleGoal();

            context.Goals.Add(goal);
            context.SaveChanges();
        }

        private void ChangeTask(int id, TaskBindingModel model)
        {
            //TODO
        }

        private void ChangeGoal(int id, TaskBindingModel model)
        {
            //TODO
        }

        private void DeleteTask(int id, TaskBindingModel model)
        {
            //TODO
        }

        private void DeleteGoal(int id, TaskBindingModel model)
        {
            //TODO
        }
    }
}
