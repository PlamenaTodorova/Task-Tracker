using Models.BindingModels;
using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace DataStorage
{
    public class TaskEngin
    {
        private TaskContext context;

        internal TaskEngin(TaskContext context)
        {
            this.context = context;
            this.context.RecalculateGoalsDates();
            this.context.SaveChanges();
        }

        public ICollection<TaskViewModel> GetTasks(DateTime date)
        {
            DateTime deadline = date.AddDays(Constants.NumberOfDays);
            List<TaskViewModel> views = new List<TaskViewModel>();
            List<Task> currentTasks = this.context.Tasks
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

            foreach (Goal goal in context.Goals)
            {
                if (this.CurrentlyUnfished(goal, date))
                {
                    views.Add(this.GenerateView(goal, date));
                }
            }

            views.Sort();
            return views;
        }

        public ICollection<HistoryViewModel> GetOldTasks(DateTime date)
        {
            List<HistoryViewModel> views = new List<HistoryViewModel>();
            List<Task> currentTasks = context.Tasks
                .Where(t => t.Deadline.Year == date.Year && t.Deadline.Month == date.Month && t.Deadline.Day == date.Day)
                .ToList();

            foreach (Task task in currentTasks)
            {
                HistoryViewModel model = new HistoryViewModel(this.GenerateView(task));
                model.IsFinishedPath = task.IsFinished ? Constants.FinishedIcon : Constants.UnfinishedIcon;
                views.Add(model);
            }

            views.Sort();
            return views;
        }

        public ICollection<HistoryViewModel> GetOldGoals(DateTime date)
        {
            List<HistoryViewModel> views = new List<HistoryViewModel>();
            List<Goal> suitable = context.Goals.Where(g => g.OriginalDate <= date).ToList();

            foreach (Goal goal in suitable)
            {
                HistoryViewModel model = new HistoryViewModel(this.GenerateView(goal, date));

                model.Deadline = goal.SetBack(model.Deadline, date);
                DateTime beginPeriod = goal.LastPeriod(model.Deadline);
                bool flag = context.GoalsLog
                    .Where(l => l.Date > beginPeriod && l.Date <= model.Deadline && l.Goal.Id == goal.Id)
                    .Count() != 0;
                model.IsFinishedPath =  flag ? Constants.FinishedIcon : Constants.UnfinishedIcon;
                views.Add(model);
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

        public ICollection<TomorrowViewModel> GetFutureTask(DateTime date)
        {
            DateTime deadline = date.AddDays(Constants.NumberOfDays);
            List<TomorrowViewModel> views = new List<TomorrowViewModel>();
            List<Task> currentTasks = this.context.Tasks
                .Where(t => t.Deadline <= deadline && t.Deadline >= date && !t.IsFinished)
                .ToList();

            foreach (Task task in currentTasks)
            {
                TomorrowViewModel model = new TomorrowViewModel(this.GenerateView(task), true);
                views.Add(model);
            }

            views.Sort();
            return views;
        }
    
        public ICollection<TomorrowViewModel> GetFutureGoal(DateTime date)
        {
            List<TomorrowViewModel> views = new List<TomorrowViewModel>();
            List<Goal> suitable = context.Goals.ToList();

            foreach (Goal goal in suitable)
            {
                TaskViewModel baseModel = this.GenerateView(goal, date);
                bool isFinished = IsGoalFinished(goal, baseModel.Deadline);
                TomorrowViewModel model = 
                    new TomorrowViewModel(baseModel, isFinished);

                views.Add(model);
            }

            views.Sort();
            return views;
        }

        private bool IsGoalFinished(Goal goal, DateTime date)
        {
            DateTime beginPeriod = goal.LastPeriod(date);
            bool result = context.GoalsLog
                .Where(l => l.Date > beginPeriod && l.Date <= date && l.Goal.Id == goal.Id)
                .Count() != 0;

            return result;
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

        public TaskViewModel Change(int id, string type, TaskBindingModel model, DateTime date)
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
            {
                Goal goal = context.Goals.Find(id);
                if (!this.CurrentlyUnfished(goal, date))
                    return null;

                return this.GenerateView(goal, date);
            }
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

        private bool CurrentlyUnfished(Goal goal, DateTime date)
        {
            DateTime lastDeadline = goal.LastPeriod();

            if (lastDeadline < date)
                return true;
            return false;
        }

        private List<TaskViewModel> GetAll(List<Task> currentTasks)
        {
            List<TaskViewModel> views = new List<TaskViewModel>();

            foreach (Task task in currentTasks)
            {
                TaskViewModel view = GenerateView(task);

                views.Add(view);
            }

            return views;
        }

        private TaskViewModel GenerateView(Task task)
        {
            TaskViewModel view = new TaskViewModel(task.Type.Name == "Apointment");

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
            TaskViewModel view = new TaskViewModel(false);

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
                Span = model.Period,
                Description = model.Description,
            };

            goal.SetDate(DateTime.Today);

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

            this.AddToLog(goal, model.Deadline);

            goal.RescheduleGoal();
            model.Deadline = goal.Deadline;

            context.SaveChanges();
        }

        private void AddToLog(Goal goal, DateTime date)
        {
            LogEntry entry = new LogEntry();
            entry.Date = date;
            entry.Goal = goal;

            this.context.GoalsLog.Add(entry);
            this.context.SaveChanges();
        }
    }
}
