using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DatabaseModels
{
    public enum Periods
    {
        Day,
        Week,
        Month,
        Year
    }

    public class Goal : BaseTask
    {
        public Periods Span { get; set; }

        public uint SuccessfullyFinished { get; set; }

        public uint TotalChalanges { get; set; }
    }
}
