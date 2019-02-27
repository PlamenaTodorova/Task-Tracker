using Models.DatabaseModels;
using System.Collections.Generic;
using Utilities;

namespace DataStorage
{
    internal enum CollectionTypes
    {
        Tasks,
        Goals,
        Finished
    };

    internal class DataStorage
    {
        public DataStorage()
        {
            Tasks = JSONParser<List<Task>>.ReadFile(Constants.TasksFile);
            Goals = JSONParser<List<Goal>>.ReadFile(Constants.GoalsFile);
            FinishedTasks = JSONParser<List<Task>>.ReadFile(Constants.FinishedFile);
        }

        public List<Task> Tasks { get; set; }

        public List<Goal> Goals { get; set; }

        public List<Task> FinishedTasks { get; set; }

        public void SaveChanges(CollectionTypes types)
        {
            switch(types)
            {
                case CollectionTypes.Tasks:
                    SaveTasks();
                    break;
                case CollectionTypes.Goals:
                    SaveGoals();
                    break;
                case CollectionTypes.Finished:
                    SaveFinished();
                    break;
            }
        }

        private void SaveGoals()
        {
            JSONParser<List<Goal>>.WriteJson(this.Goals, Constants.GoalsFile);
        }

        private void SaveTasks()
        {
            JSONParser<List<Task>>.WriteJson(this.Tasks, Constants.TasksFile);
        }

        private void SaveFinished()
        {
            JSONParser<List<Task>>.WriteJson(this.FinishedTasks, Constants.FinishedFile);
        }
    }
}
