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
    /// Interaction logic for AllTasks.xaml
    /// </summary>
    public partial class AllTasks : Page
    {
        private AllTaskController controller;

        public AllTasks()
        {
            InitializeComponent();
            this.controller = new AllTaskController();

            ObservableCollection<TaskViewModel> collection = this.controller.GetTasks();
            this.tasks.ItemsSource = collection;
            this.tasks_existance.Visibility = collection.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
