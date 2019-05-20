using Models.BindingModels;
using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace DataStorage.Engins
{ 
    public abstract class BaseTaskEngin
    {
        private TaskContext context;

        internal BaseTaskEngin(TaskContext context)
        {
            this.context = context;
        }

        //abstract
        public abstract ICollection<TaskViewModel> GetGoals(DateTime date);

        public abstract ICollection<TaskViewModel> GetTasks(DateTime date);

        //Common
        public TaskBindingModel GetTask(int id, string type)
        {
            return type == "Goal" ? new TaskBindingModel(context.Goals.Find(id)) : new TaskBindingModel(context.Tasks.Find(id));
        }

        public DateTime Check(int id, TaskViewModel model)
        {
            if (model.Type == "Goal")
                this.CheckGoal(id, model);
            else this.CheckTask(id);

            return model.Deadline;
        }

        public void Delete(int id, string type)
        {
            if (type == "Goal")
                this.DeleteGoal(id);
            else if (type == "Appointment")
                this.DeleteAppointment(id);
            else
                this.DeleteTask(id);
        }

        public TaskViewModel Change(int id, string type, TaskBindingModel model, DateTime date)
        {
            if (type != model.TaskType && (type == "Goal" || model.TaskType == "Goal"
                || type == "Appointment" || model.TaskType == "Appointment"))
            {
                this.Delete(id, type);
                id = SharedEnginFunctions.Add(this.Context, model);
            }
            else if (type == "Goal")
                this.ChangeGoal(id, model);
            else if (type == "Appointment")
                this.ChangeAppointment(id, model);
            else
                this.ChangeTask(id, model);

            if (type == "Goal")
            {
                Goal goal = context.Goals.Find(id);
                if (!this.CurrentlyUnfished(goal, date))
                    return null;

                return this.GenerateView(goal, date);
            }
            else if (type == "Appointment")
                return this.GenerateView(context.Appointments.Find(id));
            else
                return this.GenerateView(context.Tasks.Find(id));
        }

        //Used by the subclasses
        protected TaskContext Context
        {
            get
            {
                return this.context;
            }
        }

        protected bool IsGoalFinished(Goal goal, DateTime date)
        {
            DateTime beginPeriod = goal.LastPeriod(date);
            bool result = context.GoalsLog
                .Where(l => l.Date > beginPeriod && l.Date <= date && l.Goal.Id == goal.Id)
                .Count() != 0;

            return result;
        }

        protected bool CurrentlyUnfished(Goal goal, DateTime date)
        {
            DateTime lastDeadline = goal.LastPeriod();

            if (lastDeadline < date)
                return true;
            return false;
        }

        protected TaskViewModel GenerateView(Task task)
        {
            TaskViewModel view = new TaskViewModel(false);

            view.Id = task.Id.ToString() + ":" + task.Type.Name;
            view.Name = task.Name;
            view.Deadline = task.Deadline;
            view.Description = task.Description;
            view.Type = task.Type.Name;
            view.PicturePath = task.Type.PicturePath;

            return view;
        }

        protected TaskViewModel GenerateView(Goal goal, DateTime date)
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

        protected TaskViewModel GenerateView(BaseTask task)
        {
            TaskViewModel view = new TaskViewModel(true);

            view.Id = task.Id.ToString() + ":App";
            view.Name = task.Name;
            view.Deadline = task.Deadline;
            view.Description = task.Description;
            view.Type = "Appointment";
            view.PicturePath = Constants.AppointmentIcon;

            return view;
        }

        protected void AddToLog(Goal goal, DateTime date)
        {
            LogEntry entry = new LogEntry();
            entry.Date = date;
            entry.Goal = goal;

            this.context.GoalsLog.Add(entry);
            this.context.SaveChanges();
        }

        //To be overriden by subclasses
        public virtual ICollection<TypeBindingModel> GetAllTypes() { return null; }

        public virtual ICollection<TaskViewModel> GetAll(ICollection<TypeBindingModel> chosen)
            { return null; }

        protected virtual void CheckGoal(int id, TaskViewModel model)
        {
            Goal goal = context.Goals.Find(id);

            this.AddToLog(goal, model.Deadline);

            goal.RescheduleGoal();
            model.Deadline = goal.Deadline;

            context.SaveChanges();
        }

        //Used only from the base class
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

        private void DeleteAppointment(int id)
        {
            Appointment toRemove = context.Appointments.Find(id);

            context.Appointments.Remove(toRemove);
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

        private void ChangeAppointment(int id, TaskBindingModel model)
        {
            Appointment appointment = context.Appointments.Find(id);

            appointment.Name = model.Name;
            appointment.Description = model.Description;

            appointment.Deadline = new DateTime(model.Deadline.Year, model.Deadline.Month, model.Deadline.Day, 23, 59, 59);

            context.SaveChanges();
        }

        private void CheckTask(int id)
        {
            context.Tasks.Find(id).IsFinished = true;
            context.SaveChanges();
        }
    }
}