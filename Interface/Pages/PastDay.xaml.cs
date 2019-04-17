using Interface.Controllers;
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
    /// Interaction logic for PastDay.xaml
    /// </summary>
    public partial class PastDay : Page
    {
        private PastTasksController controller;
        public PastDay(DateTime date)
        {
            this.controller = new PastTasksController(date);
            
            InitializeComponent();
            this.tasks.ItemsSource = this.controller.GetTasks();
            this.goals.ItemsSource = this.controller.GetGoals();
            this.SetEmptyFields();
        }

        public void Check(object sender, RoutedEventArgs e)
        {
            string id = ((TextBlock)((Grid)((Button)sender).Parent).FindName("Id")).Text;

            
        }

        private void SetEmptyFields()
        {
            this.tasks_existance.Visibility = this.tasks.ItemsSource.OfType<object>().Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
            this.goals_existance.Visibility = this.goals.ItemsSource.OfType<object>().Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
