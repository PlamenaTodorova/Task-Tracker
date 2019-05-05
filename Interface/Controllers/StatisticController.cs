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
    public class StatisticController
    {
        private ObservableCollection<StatViewModel> stat;

        public StatisticController()
        {
            this.stat = new ObservableCollection<StatViewModel>(Engin.GetEngin().GetStatistic().GetStats());
        }

        public ObservableCollection<StatViewModel> GetStats()
        {
            return stat;
        }
    }
}
