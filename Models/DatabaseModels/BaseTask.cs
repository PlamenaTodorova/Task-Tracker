using System;

namespace Models.DatabaseModels
{
    public abstract class BaseTask 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Deadline { get; set; }

        public string Description { get; set; }
    }
}
