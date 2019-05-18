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
            :base(taskViewModel)
        {
        }

        public string IsFinishedPath { get; set; }

        public int CompareTo(HistoryViewModel other)
        {
            return base.CompareTo(other);
        }
    }
}
