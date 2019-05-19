using DataStorage;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for DayFrame.xaml
    /// </summary>
    public partial class DayFrame : Page
    {
        DateViewModel date;

        public DayFrame()
        {
            InitializeComponent();
            this.date = new DateViewModel();
            this.DataContext = date;
            this.ChangeFrame();
        }

        private void ChangeFrame()
        {
            if (date.Current < DateTime.Today)
            {
                Engin.GetEngin().ChangeContext(date.Current);
                this.frameHolder.Navigate(new PastDay(date.Current));
            }
            else if (date.Current == DateTime.Today)
            {
                Engin.GetEngin().ChangeContext(date.Current);
                this.frameHolder.Navigate(new OneDay(date.Current));
            }
            else
            {
                Engin.GetEngin().ChangeContext(date.Current);
                this.frameHolder.Navigate(new Tomorrow(date.Current));
            }               
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            if (this.date.Current >= DateTime.Today.AddDays(-10))
            {
                this.date.AddDays(-1);
                this.ChangeFrame();
            }
        }

        private void MoveForward(object sender, RoutedEventArgs e)
        {
            if (this.date.Current < DateTime.Today.AddDays(10))
            {
                this.date.AddDays(1);
                this.ChangeFrame();
            }
        }

    }
}
