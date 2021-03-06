﻿using DataStorage;
using Models.BindingModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Interface.Controllers
{
    internal class AllTaskController : ShowTaskController
    {
        public AllTaskController(ICollection<TypeBindingModel> model) 
            : base(DateTime.Today, model)
        {
        }

        protected override void GenerateTasks()
        {
            List<TaskViewModel> tasks = Engin.GetEngin().GetTasksEngin().GetAll(this.model).ToList();
            this.tasks = new ObservableCollection<TaskViewModel>(tasks);
        }

        protected override void ReAddGoal(TaskViewModel model)
        {
            HelperFunctions.RemoveElement<TaskViewModel>(this.goals, model);
            TaskViewModel other = this.tasks.FirstOrDefault(e => e.Id == model.Id);
            HelperFunctions.RemoveElement<TaskViewModel>(this.tasks, other);
        }

        protected override void RaAddModel(TaskViewModel changed)
        {
            if (changed.Type == "Goal")
                HelperFunctions.PutInTheRightPlace<TaskViewModel>(this.goals, changed);

            HelperFunctions.PutInTheRightPlace<TaskViewModel>(this.tasks, changed);
        }

    }
}
