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
using Interface.Controllers;
using Interface.Dialogs;
using Interface.Pages;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainController controller;

        public MainWindow()
        {
            InitializeComponent();
            this.PageHolder.Navigate(new DayFrame());
            controller = new MainController();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; 
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SwitchToDaily(object sender, RoutedEventArgs e)
        {
            this.PageHolder.Navigate(new DayFrame());
        }

        private void OpenNewTaskDialog(object sender, RoutedEventArgs e)
        {
            NewTaskDialog newTask = new NewTaskDialog();

            newTask.ShowDialog();

            if (newTask.DialogResult == true)
            {
                controller.AddTask(newTask.GetTask());
            }

            this.PageHolder.Navigate(new DayFrame());
        }

        private void SwitchToAllTasks(object sender, RoutedEventArgs e)
        {
            this.PageHolder.Navigate(new Filters());
        }
    }
}
