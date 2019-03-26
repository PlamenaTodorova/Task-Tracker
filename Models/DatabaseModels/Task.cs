using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DatabaseModels
{
    public class Task : BaseTask
    {
        public Task()
        {
            this.IsFinished = false;
        }

        public virtual TaskType Type { get; set; }

        public bool IsFinished { get; set; }
    }
}
