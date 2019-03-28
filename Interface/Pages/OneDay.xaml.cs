using Interface.Controllers;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OneDay.xaml
    /// </summary>
    public partial class OneDay : Page
    {
        private OneDayController controller;

        public OneDay(DateTime date)
        {
            InitializeComponent();
            this.controller = new OneDayController(date);
            ObservableCollection<TaskViewModel> collection = this.controller.GetTasks();
            this.tasks.ItemsSource = collection;
            this.tasks_existance.Visibility = collection.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

            collection = this.controller.GetGoals();
            this.goals.ItemsSource = collection;
            this.goals_existance.Visibility = collection.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public void Check(object sender, RoutedEventArgs e)
        {
            string id = ((TextBlock)((Grid)((Button)sender).Parent).FindName("Id")).Text;

            controller.Check(id);
        }

        public void Edit(object sender, RoutedEventArgs e)
        {

        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            string id = ((TextBlock)((Grid)((StackPanel)((Button)sender).Parent).Parent).FindName("Id")).Text;

            controller.DeleteTask(id);
        }
    }
}
