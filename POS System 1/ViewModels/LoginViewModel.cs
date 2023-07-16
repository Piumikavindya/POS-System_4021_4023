using CommunityToolkit.Mvvm.Input;
using POS_System_1.DatabaseContext;
using POS_System_1.Models;
using POS_System_1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using RelayCommand = POS_System_1.Helpers.RelayCommand;

namespace POS_System_1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;


        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand LoginCommand { get; }
    public RelayCommand AdminLoginCommand { get; }
        public RelayCommand UserLoginCommand { get; }


        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            AdminLoginCommand = new RelayCommand(AdminLogin);
            UserLoginCommand = new RelayCommand(UserLogin);
        }

      
        private void AdminLogin(object parameter)
        {
            var adminLoginView = new AdminLoginView();
            adminLoginView.ShowDialog();
        }
        private void UserLogin(object parameter)
        {
            var adminLoginView = new UserLoginView();
            adminLoginView.ShowDialog();
        }

        private void Login(object parameter)
        {
            string enteredUsername = Username; // Retrieve the entered username from the view model
            string enteredPassword = Password; // Retrieve the entered password from the view model

            // Add your login logic here
            if (IsValidLogin(enteredUsername, enteredPassword))
            {
                // Successful login logic
                // Navigate to the main product window
               
                var vm = new ProductViewModel();
                vm.title = "ProductView";
                ProductView window = new ProductView(vm);
                window.ShowDialog();

                // Close the login window
                Application.Current.Shutdown();
            }
            else
            {
                // Failed login logic
                // Display an error message
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            using (var dbContext = new POSContext())
            {
                // Retrieve the user from the database based on the entered username
                User user = dbContext.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    // Compare the entered password with the password stored in the user object
                    return (user.Password == password);
                }

                return false;
            }
        }

        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }

    }
}