using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataStorage.Engins
{
    public class CalendarEngin
    {
        private TaskContext context;

        public CalendarEngin(TaskContext context)
        {
            this.context = context;
        }

        public ICollection<DayViewModel> GetWeek(DateTime date)
        {
            date = this.GetFirstMonday(date);
            DateTime end = date.AddDays(6);

            List<DayViewModel> viewModels = new List<DayViewModel>();
            for (int i = 0; i < 7; i++)
            {
                DayViewModel model = new DayViewModel()
                {
                    Date = date.AddDays(i).Day,
                    Tasks = new List<DaysTaskViewModel>()
                };

                viewModels.Add(model);
            }

            foreach (Task item in this.context.Tasks.Where(e => e.Deadline >= date && e.Deadline <= end))
            {
                int index = item.Deadline.Subtract(date).Days;
                viewModels[index].Tasks.Add(new DaysTaskViewModel()
                {
                    Title = item.Name,
                    PicturePath = item.Type.PicturePath
                });
            }

            return viewModels;
        }

        public ICollection<DayViewModel> GetMonth(DateTime date)
        {
            date = this.GetFirstMonday(new DateTime(date.Year, date.Month, 1));
            DateTime end = date.AddDays(6);

            List<DayViewModel> viewModels = new List<DayViewModel>();
            for (int i = 0; i < 35; i++)
            {
                DayViewModel model = new DayViewModel()
                {
                    Date = date.AddDays(i).Day,
                    Tasks = new List<DaysTaskViewModel>()
                };

                viewModels.Add(model);
            }

            foreach (Task item in this.context.Tasks.Where(e => e.Deadline >= date && e.Deadline <= end))
            {
                int index = item.Deadline.Subtract(date).Days;
                viewModels[index].Tasks.Add(new DaysTaskViewModel()
                {
                    Title = item.Name,
                    PicturePath = item.Type.PicturePath
                });
            }

            return viewModels;
        }

        private DateTime GetFirstMonday(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return date;
                case DayOfWeek.Tuesday:
                    return date.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return date.AddDays(-2);
                case DayOfWeek.Thursday:
                    return date.AddDays(-3);
                case DayOfWeek.Friday:
                    return date.AddDays(-4);
                case DayOfWeek.Saturday:
                    return date.AddDays(-5);
                case DayOfWeek.Sunday:
                    return date.AddDays(-6);
            }

            return date;
        }
    }
}