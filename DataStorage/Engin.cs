using Models.BindingModels;
using Models.DatabaseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public class Engin
    {
        private DataStorage dataStorage;
        private Engin engin;

        private Engin()
        {
            this.dataStorage = new DataStorage();
        }

        public Engin GetEngin()
        {
            if (engin == null)
                engin = new Engin();

            return engin;
        }

        public ICollection<TaskViewModel> GetTasks(DateTime date)
        {
            throw new NotImplementedException();
            //TODO
        }

        public ICollection<TaskViewModel> GetGoals(DateTime date)
        {
            throw new NotImplementedException();
            //TODO
        }

        public ICollection<TaskViewModel> GetAll()
        {
            throw new NotImplementedException();
            //TODO
        }

        public ICollection<TaskViewModel> GetAll(object[] filters)
        {
            throw new NotImplementedException();
            //TODO
        }

        public TaskBindingModel GetTask(string id)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Add(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Change(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        public bool Delete(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        private bool AddTask(string id, TaskBindingModel model)
        {
            throw new NotImplementedException();
            //TODO
        }

        private void AddGoal(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void ChangeTask(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void ChangeGoal(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void DeleteTask(string id, TaskBindingModel model)
        {
            //TODO
        }

        private void DeleteGoal(string id, TaskBindingModel model)
        {
            //TODO
        }
    }
}
