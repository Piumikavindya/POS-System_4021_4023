using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using POS_System_1.DatabaseContext;
using POS_System_1.Models;
using POS_System_1.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using RelayCommand = POS_System_1.Helpers.RelayCommand;


namespace POS_System_1.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> products;
        private Product selectedProduct;
        private int newId;
        private string newName;
        private decimal newPrice;
        private int newQuantity;
        private int maxProductId;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public int NewId
        {
            get { return newId; }
            set
            {
                newId = value;
                OnPropertyChanged();
            }
        }

        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged();
            }
        }

        public decimal NewPrice
        {
            get { return newPrice; }
            set
            {
                newPrice = value;
                OnPropertyChanged();
            }
        }

        public int NewQuantity
        {
            get { return newQuantity; }
            set
            {
                newQuantity = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand SaleProductCommand { get; }
        public string title { get; internal set; }

        public ProductViewModel()
        {
            using (var dbContext = new POSContext())
            {
                maxProductId = dbContext.Products.Any() ? dbContext.Products.Max(p => p.Id) : 0;
            }

            // Retrieve the initial products from the database
            RefreshProducts();

            AddProductCommand = new RelayCommand(AddProduct);
            UpdateProductCommand = new RelayCommand(UpdateProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            SaleProductCommand = new RelayCommand(SaleProduct);
        }

        private void AddProduct(object parameter)
        {
            if (!string.IsNullOrEmpty(NewName) && NewPrice > 0 && NewQuantity >= 0)
            {
                try
                {
                    using (var dbContext = new POSContext())
                    {
                        // Check if the specified ID already exists in the database
                        bool idExists = dbContext.Products.Any(p => p.Id == NewId);
                        if (idExists)
                        {
                            MessageBox.Show("The specified ID already exists. Please choose a different ID.");
                            return;
                        }

                        // Create the new product
                        Product newProduct = new Product
                        {
                            Id = NewId,
                            Name = NewName,
                            Price = NewPrice,
                            Quantity = NewQuantity,
                              SaleId = 1
                        };

                        // Add the new product to the Products DbSet
                        dbContext.Products.Add(newProduct);
                        dbContext.SaveChanges();

                        // Refresh the Products collection from the database
                        RefreshProducts();
                    }

                    // Clear the input fields
                    NewId = 0;
                    NewName = string.Empty;
                    NewPrice = 0;
                    NewQuantity = 0;

                    MessageBox.Show("Product added successfully!");
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Error occurred while saving the product to the database: " + ex.InnerException?.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid product details.");
            }
        }



        private void UpdateProduct(object parameter)
        {
            if (SelectedProduct != null)
            {
                UpdateProductView updateProductView = new UpdateProductView(SelectedProduct);
                updateProductView.Closed += UpdateProductView_Closed;
                updateProductView.Show();
            }
            else
            {
                MessageBox.Show("Please Select a Product", "Warning!");
            }
        }

        private void UpdateProductView_Closed(object sender, EventArgs e)
        {
            // Refresh the Products collection from the database
            RefreshProducts();
        }



        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct != null)
            {
                try
                {
                    using (var dbContext = new POSContext())
                    {
                        // Delete the selected product from the database
                        dbContext.Products.Remove(SelectedProduct);
                        dbContext.SaveChanges();
                    }

                    // Remove the selected product from the collection
                    Products.Remove(SelectedProduct);

                    MessageBox.Show("Product deleted successfully!");
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Error occurred while deleting the product from the database: " + ex.InnerException?.Message);
                }
            }
            else
            {
                MessageBox.Show("Plese Select Product before Delete.", "Error");


            }
        }

        private void SaleProduct(object parameter)
        {

            SaleProductView saleProductView = new SaleProductView();
            // Pass the Products collection to the SaleProductView
            saleProductView.DataContext = new SaleProductViewModel(Products);
            saleProductView.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RefreshProducts()
        {
            using (var dbContext = new POSContext())
            {
                // Retrieve the latest products from the database
                Products = new ObservableCollection<Product>(dbContext.Products.ToList());
            }

        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
