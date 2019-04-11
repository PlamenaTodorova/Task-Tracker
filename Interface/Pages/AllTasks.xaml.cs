using Interface.Controllers;
using Interface.Dialogs;
using Models.BindingModels;
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

        public AllTasks(ICollection<TypeBindingModel> model)
        {
            InitializeComponent();
            this.controller = new AllTaskController(model);

            this.tasks.ItemsSource = this.controller.GetTasks();
            this.SetEmptyFields();
        }

        public void Check(object sender, RoutedEventArgs e)
        {
            string id = ((TextBlock)((Grid)((Button)sender).Parent).FindName("Id")).Text;

            controller.Check(id);
        }

        public void Edit(object sender, RoutedEventArgs e)
        {
            string idAndType = ((TextBlock)((Grid)((StackPanel)((Button)sender).Parent).Parent).FindName("Id")).Text;

            TaskBindingModel model = controller.GetBinding(idAndType);
            NewTaskDialog newTask = new NewTaskDialog(model);

            newTask.ShowDialog();

            if (newTask.DialogResult == true)
            {
                controller.ChangeTask(idAndType, newTask.GetTask());
            }
        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            string id = ((TextBlock)((Grid)((StackPanel)((Button)sender).Parent).Parent).FindName("Id")).Text;

            controller.DeleteTask(id);
            this.SetEmptyFields();
        }

        private void SetEmptyFields()
        {
            this.tasks_existance.Visibility = this.tasks.ItemsSource.OfType<object>().Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
