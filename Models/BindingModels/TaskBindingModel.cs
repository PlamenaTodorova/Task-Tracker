using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BindingModels
{
    public class TaskBindingModel : INotifyPropertyChanged
    {
        private string name;
        private DateTime deadline;
        private TaskType taskType;
        private Periods period;
        private string description;

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

        public TaskType TaskType
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
