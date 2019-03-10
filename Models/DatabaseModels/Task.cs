using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DatabaseModels
{
    public class Task : BaseTask
    {
        public TaskType Type { get; set; }
    }
}
