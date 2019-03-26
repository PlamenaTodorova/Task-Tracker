using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DatabaseModels
{
    public class TaskType
    {
        public TaskType()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
     
        [Required]
        public string Name { get; set; }

        public string PicturePath { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
