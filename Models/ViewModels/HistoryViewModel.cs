using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class HistoryViewModel : TaskViewModel, IComparable<HistoryViewModel>
    {
        public HistoryViewModel(TaskViewModel taskViewModel)
        {
            this.Id = taskViewModel.Id;
            this.Name = taskViewModel.Name;
            this.Deadline = taskViewModel.Deadline;
            this.Type = taskViewModel.Type;
            this.Description = taskViewModel.Description;
            this.PicturePath = taskViewModel.PicturePath;
        }

        public string IsFinishedPath { get; set; }

        public int CompareTo(HistoryViewModel other)
        {
            return base.CompareTo(other);
        }
    }
}
