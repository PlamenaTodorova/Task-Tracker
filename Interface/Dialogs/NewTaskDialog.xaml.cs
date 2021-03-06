﻿using DataStorage;
using Models.BindingModels;
using Models.DatabaseModels;
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
            this.model = new TaskBindingModel();
            this.LoadDialog();
        }

        public NewTaskDialog(TaskBindingModel bindingModel)
        {
            this.model = bindingModel;
            this.LoadDialog();
        }

        public TaskBindingModel GetTask()
        {
            return this.model;
        }

        private void LoadDialog()
        {
            InitializeComponent();
            this.type.ItemsSource = Engin.GetEngin().GetTypes();
            this.DataContext = model;
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            if (this.IsValid())
                this.DialogResult = true;
            else
                this.ErrorMessage();
        }

        private bool IsValid()
        {
            if (model.Name != null && model.Name != ""
                && model.TaskType != null && model.TaskType != "")
            {
                if (model.TaskType != "Goal")
                    return model.Deadline > DateTime.Today;
                else
                    return true;
            }
            return false;
        }

        private void ErrorMessage()
        {
            if (model.Name == null || model.Name == "")
                MessageBox.Show("Invalid name: No name for the task");
            else if (model.TaskType == null || model.TaskType == "")
                MessageBox.Show("Invalid type: No type chosen");
            else if (model.TaskType != "Goal" && model.Deadline <= DateTime.Today)
                MessageBox.Show("Invalid date: The date should be in the futer.");
        }

        private void CheckType(object sender, SelectionChangedEventArgs e)
        {
            if(this.type.SelectedValue.ToString() == "Goal")
            {
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
