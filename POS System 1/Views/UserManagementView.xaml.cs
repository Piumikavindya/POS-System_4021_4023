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
    /// Interaction logic for UserManagementView.xaml
    /// </summary>
    public partial class UserManagementView : Window
    {
        private UserManagementViewModel userManagementViewModel;
        public UserManagementView()
        {
            InitializeComponent();
            userManagementViewModel = new UserManagementViewModel();
            DataContext = userManagementViewModel;
        }

        public UserManagementView(UserManagementViewModel vm)
        {
        }
    }
}
