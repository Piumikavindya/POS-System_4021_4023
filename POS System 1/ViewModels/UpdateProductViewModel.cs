using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using POS_System_1.DatabaseContext;
using POS_System_1.Models;
using POS_System_1.Views;
using POS_System_1.Helpers;
using RelayCommand = POS_System_1.Helpers.RelayCommand;
using System;

namespace POS_System_1.ViewModels
{
    public class UpdateProductViewModel : INotifyPropertyChanged
    {
        private Product selectedProduct;
        private string newName;
        private decimal newPrice;
        private int newQuantity;
        private NavigationService navigationService;
        public Action GoBackAction { get; set; }
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (selectedProduct != value)
                {
                    selectedProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewName
        {
            get { return newName; }
            set
            {
                if (newName != value)
                {
                    newName = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal NewPrice
        {
            get { return newPrice; }
            set
            {
                if (newPrice != value)
                {
                    newPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NewQuantity
        {
            get { return newQuantity; }
            set
            {
                if (newQuantity != value)
                {
                    newQuantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand SaleProductCommand { get; }

        public UpdateProductViewModel(Product productToUpdate)
        {
            SelectedProduct = productToUpdate;
            NewName = productToUpdate.Name;
            NewPrice = productToUpdate.Price;
            NewQuantity = productToUpdate.Quantity;

            navigationService = new NavigationService();
            GoBackCommand = new RelayCommand(GoBack);

            UpdateCommand = new RelayCommand(UpdateProduct);
            SaleProductCommand = new RelayCommand(SaleProduct);

        }

        private void UpdateProduct(object parameter)
        {
            if (!string.IsNullOrEmpty(NewName) && NewPrice > 0 && NewQuantity >= 0)
            {
                using (var dbContext = new POSContext())
                {
                    var product = dbContext.Products.Find(SelectedProduct.Id);
                    if (product != null)
                    {
                        product.Name = NewName;
                        product.Price = NewPrice;
                        product.Quantity = NewQuantity;

                        dbContext.SaveChanges();

                        MessageBox.Show("Product updated successfully!");

                        // Close the UpdateProductView
                        if (parameter is Window window)
                            window.Close();
                    }
                    else
                    {
                        MessageBox.Show("Product not found in the database.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product.");
            }
      
        }

        private void GoBack(object parameter)
        {
            if (parameter is Window window)
            {
                var productView = new ProductView();
                var parentWindow = Window.GetWindow(window);
                parentWindow.Close();
                productView.Show();
            }
        }

        private void SaleProduct(object parameter)
        {
            SaleProductView saleProductView = new SaleProductView();
            saleProductView.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
