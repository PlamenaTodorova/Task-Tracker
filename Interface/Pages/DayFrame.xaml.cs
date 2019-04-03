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
            this.frameHolder.Navigate(new OneDay(date.Current));
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            this.date.AddDays(-1);
            this.ChangeFrame();
        }

        private void MoveForward(object sender, RoutedEventArgs e)
        {
            if (this.date.Current < DateTime.Today)
            {
                this.date.AddDays(1);
                this.ChangeFrame();
            }
        }

    }
}
