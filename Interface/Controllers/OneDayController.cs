﻿using DataStorage;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Controllers
{
    internal class OneDayController : ShowTaskController
    {
        public OneDayController(DateTime date)
            :base(date)
        {
        }

        protected override void GenerateTasks()
        {
            List<TaskViewModel> tasks = Engin.GetEngin().GetTasks(this.date).ToList();
            this.tasks = new ObservableCollection<TaskViewModel>(tasks);
        }
    }
}
