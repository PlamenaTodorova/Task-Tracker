using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class DayViewModel
    {
        public int Date { get; set; }

        public ICollection<DaysTaskViewModel> tasks { get; set; }
    }
}
