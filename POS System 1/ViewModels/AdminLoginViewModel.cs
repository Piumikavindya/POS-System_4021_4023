using POS_System_1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using POS_System_1.Views;
using POS_System_1.Helpers;

namespace POS_System_1.ViewModels
{
    public class AdminLoginViewModel : INotifyPropertyChanged
    {
        private string enteredPassword;
        internal string title;

        public string EnteredPassword
        {
            get { return enteredPassword; }
            set
            {
                if (enteredPassword != value)
                {
                    enteredPassword = value;
                    OnPropertyChanged(nameof(EnteredPassword));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SubmitPasswordCommand { get; }

        public AdminLoginViewModel()
        {
            SubmitPasswordCommand = new RelayCommand(SubmitPassword);
        }

        

        private void SubmitPassword(object parameter)
        {
            // Validate the entered password
            if (EnteredPassword == "123")
            {
                var vm = new UserManagementViewModel();
                vm.Title = "User Management";

                // Create and show the UserManagementView
                var userManagementView = new UserManagementView();
                userManagementView.DataContext = vm;
                userManagementView.Show();

              
            }
            else
            {
                MessageBox.Show("Invalid password. Please try again.");
            }
        }
    }
}
