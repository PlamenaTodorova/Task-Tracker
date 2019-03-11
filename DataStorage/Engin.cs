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
            DateTime deadline = date.AddDays(10);
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

        public ICollection<TaskViewModel> GetGoals()
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach(Goal goal in context.Goals)
            {
                TaskViewModel view = new TaskViewModel();

                view.Name = goal.Name;
                view.Deadline = goal.Deadline;
                view.Description = goal.Description;
                view.Type = "Goal";
                view.PicturePath = Constants.GoalsIcon;

                views.Add(view);
            }

            return views;
        }

        public ICollection<TaskViewModel> GetAll()
        {
            throw new NotImplementedException();
            //TODO
        }

        public ICollection<TaskViewModel> GetAll(object[] filters)
        {
            throw new NotImplementedException();
            //TODO
        }

        public TaskBindingModel GetTask(string id)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Add(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Change(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Delete(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        private bool AddTask(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        private void AddGoal(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void ChangeTask(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void ChangeGoal(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void DeleteTask(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void DeleteGoal(string id, TaskBindingModel model)
        {
            //TODO
        }
    }
}
