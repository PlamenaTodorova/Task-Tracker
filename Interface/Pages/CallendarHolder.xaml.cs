﻿using System;
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
    /// Interaction logic for Callendar.xaml
    /// </summary>
    public partial class CallendarHolder : Page
    {
        public CallendarHolder()
        {
            InitializeComponent();
            this.calendarFrame.Navigate(new Month());
        }

        private void SwitchToMonth(object sender, RoutedEventArgs e)
        {
            this.calendarFrame.Navigate(new Month());
        }

        private void SwitchToWeek(object sender, RoutedEventArgs e)
        {
            this.calendarFrame.Navigate(new Week());
        }
    }
}
