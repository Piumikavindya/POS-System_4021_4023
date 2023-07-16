using POS_System_1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using POS_System_1.ViewModels;

namespace POS_System_1.Helpers
{
    public class NavigationService
    {


        public void NavigateToProductView()
        {
            var productView = new ProductView();
            Application.Current.MainWindow.Close();
            productView.Show();
        }
    }
}
