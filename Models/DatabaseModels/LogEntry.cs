using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DatabaseModels
{
    public class LogEntry
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual Goal Goal { get; set; }
    }
}
