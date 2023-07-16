using POS_System_1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using POS_System_1.Views;
using RelayCommand = POS_System_1.Helpers.RelayCommand;
using POS_System_1.DatabaseContext;
using System.Linq;

namespace POS_System_1.ViewModels
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        private string newUsername;
        private string newPassword;
        private bool isAdmin;
        private ObservableCollection<User> users;
        private ICommand createUserCommand;
        private ICommand closeCommand;
        private ICommand deleteUserCommand;
        private string title;
        private User selectedUser;

        public string NewUsername
        {
            get { return newUsername; }
            set
            {
                if (newUsername != value)
                {
                    newUsername = value;
                    OnPropertyChanged(nameof(NewUsername));
                }
            }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }

    

        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                if (isAdmin != value)
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

       

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public ICommand CreateUserCommand => createUserCommand ?? (createUserCommand = new RelayCommand(CreateUser));

        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(CloseWindow));

        public ICommand DeleteUserCommand => deleteUserCommand ?? (deleteUserCommand = new RelayCommand(DeleteUser));

      


        public UserManagementViewModel()
        {
            
           
            
                // Initialize the Users collection
                Users = new ObservableCollection<User>();

                using (var dbContext = new POSContext())
                {
                    // Fetch all users from the database and add them to the collection
                    var users = dbContext.Users.ToList();
                    foreach (var user in users)
                    {
                        Users.Add(user);
                    }
                }
            


        }

        private void CreateUser(object parameter)
        {
            if (!string.IsNullOrEmpty(NewUsername) && !string.IsNullOrEmpty(NewPassword))
            {
                User newUser = new User
                {
                    Username = NewUsername,
                    Password = NewPassword,
                    IsAdmin = false, // Assign a default value
                    SaleId = 1
                };

                using (var dbContext = new POSContext())
                {
                    // Add the new user to the Users DbSet
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();
                }
                RefreshUsers();

                // Clear the input fields
                NewUsername = string.Empty;
                NewPassword = string.Empty;

                // Refresh the Users collection
                RefreshUsers();

                MessageBox.Show("User created successfully!");
            }
            else
            {
                MessageBox.Show("Please enter a username and password.");
            }
        }

        private void DeleteUser(object parameter)
        {
            if (SelectedUser != null)
            {
                using (var dbContext = new POSContext())
                {
                    // Remove the selected user from the database
                    dbContext.Users.Remove(SelectedUser);
                    dbContext.SaveChanges();
                }

                // Remove the selected user from the Users collection
                Users.Remove(SelectedUser);
            }
            else
            {
                MessageBox.Show("Please select a user.");
            }
        }




        private void RefreshUsers()
        {
            using (var dbContext = new POSContext())
            {
                // Retrieve the latest users from the database
                Users = new ObservableCollection<User>(dbContext.Users.ToList());
            }
        }


        private void CloseWindow(object parameter)
        {
            Application.Current.MainWindow.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
