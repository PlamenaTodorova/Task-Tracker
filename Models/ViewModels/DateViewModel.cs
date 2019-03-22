using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class DateViewModel : INotifyPropertyChanged
    {
        private DateTime current;

        public DateViewModel()
        {
            this.Current = DateTime.Today;
        }

        public void AddDays(int days)
        {
            this.Current = this.Current.AddDays(days);
        }

        public DateTime Current
        {
            get { return this.current; }
            set
            {
                if (this.current != value)
                {
                    this.current = value;
                    this.NotifyPropertyChanged("Current");
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
