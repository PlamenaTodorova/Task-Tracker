using Models.DatabaseModels;
using System;
using System.Windows;

namespace Models.ViewModels
{
    public class TaskViewModel : IComparable<TaskViewModel>
    {
        public TaskViewModel(bool isAppointment)
        {
            this.IsAppointment = isAppointment ? Visibility.Hidden : Visibility.Visible;
        }

        public TaskViewModel(TaskViewModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Type = model.Type;
            this.PicturePath = model.PicturePath;
            this.Deadline = model.Deadline;
            this.Description = model.Description;
            this.IsAppointment = model.IsAppointment;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string PicturePath { get; set; }

        public DateTime Deadline { get; set; }

        public string Description { get; set; }

        public Visibility IsAppointment { get; set; }

        public int CompareTo(TaskViewModel other)
        {
            return this.Deadline.CompareTo(other.Deadline);
        }
    }
}
