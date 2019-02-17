using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DatabaseModels
{
    public enum TaskType
    {
        Work,
        School,
        Social,
        Apointment
    }

    public class Task : BaseTask
    {
        TaskType type { get; set; }
    }
}
