﻿using Models.DatabaseModels;
using System;

namespace Models.ViewModels
{
    public class TaskViewModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string PicturePath { get; set; }

        public DateTime Dateline { get; set; }

        public string Description { get; set; }
    }
}
