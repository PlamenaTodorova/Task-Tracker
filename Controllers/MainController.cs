using DataStorage;
using Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Controllers
{
    public class MainController
    {
        public void AddTask(TaskBindingModel model)
        {
            Engin.GetEngin().Add(model);
        }
    }
}
