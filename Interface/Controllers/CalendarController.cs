using DataStorage;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Controllers
{
    public class CallendarController
    {
        private ObservableCollection<DayViewModel> week;
        private ObservableCollection<DayViewModel> month;

        public CallendarController(DateTime date)
        {
            this.week = new ObservableCollection<DayViewModel>(
                Engin.GetEngin().GetCalendar().GetWeek(date));
            this.month = new ObservableCollection<DayViewModel>(
                Engin.GetEngin().GetCalendar().GetMonth(date));
        }

        ObservableCollection<DayViewModel> GetWeek()
        {
            return this.week;
        }

        ObservableCollection<DayViewModel> GetMonth()
        {
            return this.month;
        }
    }
}
