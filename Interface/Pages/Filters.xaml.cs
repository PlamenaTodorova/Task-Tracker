﻿using DataStorage;
using Models.BindingModels;
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
    /// Interaction logic for Filters.xaml
    /// </summary>
    public partial class Filters : Page
    {
        private ObservableCollection<TypeBindingModel> models;
        public Filters()
        {
            models = new ObservableCollection<TypeBindingModel>(Engin.GetEngin().GetAllTypes());
            InitializeComponent();
            this.filters.ItemsSource = models;
            this.allTask.Navigate(new AllTasks(models));
        }
    }
}
