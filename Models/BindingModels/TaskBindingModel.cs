using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Models.BindingModels
{
    public class TaskBindingModel : INotifyPropertyChanged
    {
        private string name;
        private DateTime deadline;
        private string taskType;
        private Periods period;
        private string description;

        public TaskBindingModel()
        {
            this.deadline = DateTime.Today;
        }

        public TaskBindingModel(Goal goal)
        {
            this.name = goal.Name;
            this.deadline = goal.Deadline;
            this.period = goal.Span;
            this.taskType = "Goal";
            this.description = goal.Description;
        }

        public TaskBindingModel(Task task)
        {
            this.name = task.Name;
            this.deadline = task.Deadline;
            this.taskType = task.Type.Name;
            this.description = task.Description;
        }

        public TaskBindingModel(Appointment appointment)
        {
            this.name = appointment.Name;
            this.deadline = appointment.Deadline;
            this.taskType = "Appointment";
            this.description = appointment.Description;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public DateTime Deadline
        {
            get { return this.deadline; }
            set
            {
                if (this.deadline != value)
                {
                    this.deadline = value;
                    this.NotifyPropertyChanged("Deadline");
                }
            }
        }

        public string TaskType
        {
            get { return this.taskType; }
            set
            {
                if (this.taskType != value)
                {
                    this.taskType = value;
                    this.NotifyPropertyChanged("TaskType");
                }
            }
        }

        public Periods Period
        {
            get { return this.period; }
            set
            {
                if (this.period != value)
                {
                    this.period = value;
                    this.NotifyPropertyChanged("Period");
                }
            }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
