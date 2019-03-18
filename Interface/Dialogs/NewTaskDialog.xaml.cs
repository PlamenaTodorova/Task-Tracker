using DataStorage;
using Models.BindingModels;
using Models.DatabaseModels;
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
using System.Windows.Shapes;

namespace Interface.Dialogs
{
    /// <summary>
    /// Interaction logic for NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        private TaskBindingModel model;

        public NewTaskDialog()
        {
            InitializeComponent();
            this.type.ItemsSource = Engin.GetEngin().GetTypes();
            this.model = new TaskBindingModel();
            this.DataContext = model;
        }

        public TaskBindingModel GetTask()
        {
            return this.model;
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckType(object sender, SelectionChangedEventArgs e)
        {
            if(this.type.SelectedValue.ToString() == "Goal")
            {
                MessageBox.Show(this.type.SelectedValue.ToString());
                this.deadline.IsEnabled = false;
                this.period.IsEnabled = true;
            }
            else
            {
                this.deadline.IsEnabled = true;
                this.period.IsEnabled = false;
            }
        }
    }
}
