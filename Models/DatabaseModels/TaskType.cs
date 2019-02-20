using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DatabaseModels
{
    public class TaskType
    {
        public TaskType()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PicturePath { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
