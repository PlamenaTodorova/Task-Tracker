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
        public Goal()
        {
            this.FinishedTasks = new HashSet<LogEntry>();
        }

        public Periods Span { get; set; }

        public DateTime OriginalDate { get; set; }
        
        public virtual ICollection<LogEntry> FinishedTasks { get; set; }

        public void SetDate(DateTime current)
        {
            DateTime tomorrow = current.AddDays(1);

            this.Deadline = current;
            this.RescheduleGoal();
            this.OriginalDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0);
        }

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

        public DateTime LastPeriod()
        {
            switch (this.Span)
            {
                case Periods.Day:
                    return this.Deadline.AddDays(-1);
                case Periods.Week:
                    return this.Deadline.AddDays(-7);
                case Periods.Month:
                    return this.Deadline.AddMonths(-1);
                case Periods.Year:
                    return this.Deadline.AddYears(-1);
                default:
                    return this.Deadline;
            }
        }
    }
}
