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

        public uint TotalChallanges { get; set; }

        public void RescheduleGoal()
        {
            this.Deadline = this.Recalculate(this.Deadline);
        }

        public DateTime RecalculateDate(DateTime current)
        {
            DateTime result = this.Deadline;
            DateTime other = this.Recalculate(this.Deadline);
            while (result <= current && other <= current)
            {
                result = other;
                other = this.Recalculate(result);
            }
            return result;
        }

        private DateTime Recalculate(DateTime date)
        {
            switch (this.Span)
            {
                case Periods.Day:
                    return date.AddDays(1);
                case Periods.Week:
                    return date.AddDays(7);
                case Periods.Month:
                    return date.AddMonths(1);
                case Periods.Year:
                    return date.AddYears(1);
                default:
                    return this.Deadline;
            }
        }
    }
}
