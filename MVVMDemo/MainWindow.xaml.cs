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
using MVVMDemo.ViewModels;
namespace MVVMDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RobotViewModel ViewModel;
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new RobotViewModel();
            this.DataContext = ViewModel;
        }

        private void RobotView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
