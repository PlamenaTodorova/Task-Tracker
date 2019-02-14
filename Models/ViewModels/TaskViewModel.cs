using System;

namespace Models.ViewModels
{
    public enum ViewType
    {
        Work,
        School,
        Social,
        Apointment,
        Goal
    }

    public class TaskViewModel
    {
        public string Name { get; set; }

        public ViewType Type { get; set; }

        public DateTime Dateline { get; set; }

        public string Description { get; set; }
    }
}
