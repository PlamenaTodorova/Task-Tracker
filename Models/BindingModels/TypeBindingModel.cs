using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BindingModels
{
    public class TypeBindingModel : INotifyPropertyChanged
    {
        private int id;
        private bool chosen;
        private string name;

        public TypeBindingModel(string name, int id)
        {
            this.Id = id;
            this.name = name;
            this.chosen = true;
        }

        public int Id
        {
            get { return this.id; }
            private set { this.id = value; }
        }

        public bool Chosen
        {
            get { return this.chosen; }
            set
            {
                if (this.chosen != value)
                {
                    this.chosen = value;
                    this.NotifyPropertyChanged("Chosen");
                }
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
