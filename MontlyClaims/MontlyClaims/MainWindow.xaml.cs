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

namespace MontlyClaims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLecturer_Click(object sender, RoutedEventArgs e)
        {
            
            LecturerWindow lecturerWindow = new LecturerWindow();
            lecturerWindow.Show();
            this.Close();
        }

        private void btnCoordinator_Click(object sender, RoutedEventArgs e)
        {
            
            CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
            coordinatorWindow.Show();
            this.Close();
        }
    }
}
