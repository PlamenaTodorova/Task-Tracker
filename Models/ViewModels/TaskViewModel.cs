using Models.DatabaseModels;
using System;

namespace Models.ViewModels
{
    public class TaskViewModel : IComparable<TaskViewModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string PicturePath { get; set; }

        public DateTime Deadline { get; set; }

        public string Description { get; set; }

        public int CompareTo(TaskViewModel other)
        {
            return this.Deadline.CompareTo(other.Deadline);
        }
    }
}
