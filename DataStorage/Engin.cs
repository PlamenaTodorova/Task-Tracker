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
            this.context.RecalculateGoalsDates();
            this.context.SaveChanges();
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
                .Where(t => t.Deadline <= deadline && t.Deadline >= date && !t.IsFinished)
                .ToList();

            foreach (Task task in currentTasks)
            {
                views.Add(this.GenerateView(task));
            }

            views.Sort();
            return views;
        }

        public ICollection<TaskViewModel> GetGoals(DateTime date)
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach(Goal goal in context.Goals)
            {
                views.Add(this.GenerateView(goal, date));
            }

            views.Sort();
            return views;
        }

        public ICollection<TaskViewModel> GetAll(ICollection<TypeBindingModel> chosen)
        {
            List<int> indexes = chosen
                .Where(e => e.Chosen)
                .Select(e => e.Id)
                .ToList();

            List<Task> tasks = context.Tasks
                .Where(e => indexes.Contains(e.Type.Id) && e.IsFinished == false)
                .ToList();

            List<TaskViewModel> views = this.GetAll(tasks);

            if (indexes.Contains(-1))
            {
                views.AddRange(this.GetGoals(DateTime.Today));
            }

            views.Sort();
            return views;
        }

        public ICollection<TypeBindingModel> GetAllTypes()
        {
            List<TypeBindingModel> bind = new List<TypeBindingModel>();

            foreach (TaskType type in context.Type)
            {
                bind.Add(new TypeBindingModel(type.Name, type.Id));
            }

            bind.Add(new TypeBindingModel("Goal", -1));

            return bind;
        }

        public ICollection<string> GetTypes()
        {
            List<string> types = context.Type
                .Select(e => e.Name)
                .ToList();
            types.Add("Goal");
            return types;
        }

        public TaskBindingModel GetTask(int id, string type)
        {
            return type == "Goal" ? new TaskBindingModel(context.Goals.Find(id)) : new TaskBindingModel(context.Tasks.Find(id));
        }

        public int Add(TaskBindingModel model)
        {
            if (model.TaskType == "Goal")
                return this.AddGoal(model);
            else return this.AddTask(model);
        }

        public TaskViewModel Change(int id, string type, TaskBindingModel model)
        {
            if (type != model.TaskType && (type == "Goal" || model.TaskType == "Goal"))
            {
                this.Delete(id, type);
                id = this.Add(model);
            }
            else if (type == "Goal")
                this.ChangeGoal(id, model);
            else
                this.ChangeTask(id, model);

            if (type == "Goal")
                return this.GenerateView(context.Goals.Find(id), DateTime.Today);
            else
                return this.GenerateView(context.Tasks.Find(id));
        }

        public void Delete(int id, string type)
        {
            if (type == "Goal")
                this.DeleteGoal(id);
            else this.DeleteTask(id);
        }

        public DateTime Check(int id, TaskViewModel model)
        {
            if (model.Type == "Goal")
                this.CheckGoal(id, model);
            else this.CheckTask(id);

            return model.Deadline;
        }

        private List<TaskViewModel> GetAll(List<Task> currentTasks)
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach (Task task in currentTasks)
            {
                TaskViewModel view = new TaskViewModel();

                view.Id = task.Id.ToString() + ":" + task.Type.Name;
                view.Name = task.Name;
                view.Deadline = task.Deadline;
                view.Description = task.Description;
                view.Type = task.Type.Name;
                view.PicturePath = task.Type.PicturePath;

                views.Add(view);
            }

            return views;
        }

        private TaskViewModel GenerateView(Task task)
        {
            TaskViewModel view = new TaskViewModel();

            view.Id = task.Id.ToString() + ":" + task.Type.Name;
            view.Name = task.Name;
            view.Deadline = task.Deadline;
            view.Description = task.Description;
            view.Type = task.Type.Name;
            view.PicturePath = task.Type.PicturePath;

            return view;
        }

        private TaskViewModel GenerateView(Goal goal, DateTime date)
        {
            TaskViewModel view = new TaskViewModel();

            view.Id = goal.Id.ToString() + ":Goal";
            view.Name = goal.Name;
            view.Deadline = goal.RecalculateDate(date);
            view.Description = goal.Description;
            view.Type = "Goal";
            view.PicturePath = Constants.GoalsIcon;

            return view;
        }

        private int AddTask(TaskBindingModel model)
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

        private int AddGoal(TaskBindingModel model)
        {
            Goal goal = new Goal()
            {
                Name = model.Name,
                Deadline = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59),
                Span = model.Period,
                Description = model.Description,
            };
            
            goal.RescheduleGoal();

            context.Goals.Add(goal);
            context.SaveChanges();

            return goal.Id;
        }

        private void ChangeTask(int id, TaskBindingModel model)
        {
            Task task = context.Tasks.Find(id);

            task.Name = model.Name;
            task.Description = model.Description;
            task.Type = context.Type.FirstOrDefault(e => e.Name == model.TaskType);
            task.Deadline = model.Deadline;
            context.SaveChanges();
        }

        private void ChangeGoal(int id, TaskBindingModel model)
        {
            Goal goal = context.Goals.Find(id);

            goal.Name = model.Name;
            goal.Description = model.Description;
            goal.Span = model.Period;

            goal.Deadline = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

            goal.RescheduleGoal();
            context.SaveChanges();
        }

        private void DeleteTask(int id)
        {
            Task toRemove = context.Tasks.Find(id);

            context.Tasks.Remove(toRemove);
            context.SaveChanges();
        }

        private void DeleteGoal(int id)
        {
            Goal toRemove = context.Goals.Find(id);

            context.Goals.Remove(toRemove);
        }

        private void CheckTask(int id)
        {
            context.Tasks.Find(id).IsFinished = true;
            context.SaveChanges();
        }

        private void CheckGoal(int id, TaskViewModel model)
        {
            Goal goal = context.Goals.Find(id);
            goal.RescheduleGoal();
            model.Deadline = goal.Deadline;
            context.SaveChanges();
        }
    }
}
