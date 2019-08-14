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
    /// Interaction logic for Week.xaml
    /// </summary>
    public partial class Week : Page
    {
        public Week()
        {
            InitializeComponent();
            this.days.ItemsSource = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        }
    }
}
