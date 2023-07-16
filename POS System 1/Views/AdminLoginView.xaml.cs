using POS_System_1.ViewModels;
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

namespace POS_System_1.Views
{
    /// <summary>
    /// Interaction logic for AdminLoginView.xaml
    /// </summary>
    public partial class AdminLoginView : Window
    {
        private AdminLoginViewModel adminLoginViewModel;

        public AdminLoginView()
        {
            InitializeComponent(); 
            adminLoginViewModel = new AdminLoginViewModel();
            DataContext = adminLoginViewModel;


        }

        
    }
}
