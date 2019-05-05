using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class StatViewModel
    {
        public string Name { get; set; }

        public string Period { get; set; }

        public string Description { get; set; }

        public int Successful { get; set; }

        public int Total { get; set; }
    }
}
