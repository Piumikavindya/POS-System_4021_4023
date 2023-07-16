using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using POS_System_1.DatabaseContext;
using POS_System_1.Models;
using RelayCommand = POS_System_1.Helpers.RelayCommand;

namespace POS_System_1.ViewModels
{
    public class SaleProductViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _shoppingCartItems;
        public ObservableCollection<Product> ShoppingCartItems
        {
            get { return _shoppingCartItems; }
            set
            {
                _shoppingCartItems = value;
                OnPropertyChanged();
            }
        }

        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged();
            }
        }

        private int _sellingQuantity;
        public int SellingQuantity
        {
            get { return _sellingQuantity; }
            set
            {
                _sellingQuantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        private int _totalQuantity;
        public int TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                _totalQuantity = value;
                OnPropertyChanged();
            }
        }

        // Command to add a product to the cart
        public RelayCommand AddToCartCommand { get; private set; }

        // Command to complete the sale
        public RelayCommand CompleteSaleCommand { get; private set; }
        public object sale { get; private set; }

        public SaleProductViewModel(ObservableCollection<Product> products)
        {
            ShoppingCartItems = new ObservableCollection<Product>();

            // Initialize the commands
            AddToCartCommand = new RelayCommand(AddToCart);
            CompleteSaleCommand = new RelayCommand(CompleteSale);
        }

        public SaleProductViewModel()
        {
        }

        // Method to add a product to the cart
        private void AddToCart(object parameter)
        {
            // Get the product to add from the database using the ProductId
            Product productToAdd = GetProductById(ProductId);

            // Check if the product is available and has sufficient quantity
            if (productToAdd != null && productToAdd.Quantity >= SellingQuantity)
            {
                var newProduct = new Product
                {
                    Id = productToAdd.Id,
                    Name = productToAdd.Name,
                    Price = productToAdd.Price,
                  Quantity = SellingQuantity,
                    SaleId = 1,

                 
                };
              
                // Update the ShoppingCartItems, TotalPrice, and TotalQuantity properties
                ShoppingCartItems.Add(productToAdd);
                TotalPrice += productToAdd.Price * SellingQuantity;
                TotalQuantity += SellingQuantity;

                ProductId = 0;
                SellingQuantity = 0;
          
                // Show a message box or perform any other necessary logic
                MessageBox.Show("Product added to cart!");
            }
            else
            {
                MessageBox.Show("Product is unavailable or insufficient quantity!");
            }
        }

        private Product GetProductById(int productId)
        {
            using (var dbContext = new POSContext())
            {
                return dbContext.Products.FirstOrDefault(p => p.Id == productId);
            }
        }


        // Method to complete the sale
        private void CompleteSale(object parameter)
        {
            try
            {
                // Check if the cart is empty
                if (ShoppingCartItems.Count == 0)
                {
                    MessageBox.Show("The cart is empty. Please add products before completing the sale.");
                    return;
                }

                // Create a new Sale object
                Sale sale = new Sale();

                sale.ProductNames = string.Join(", ", ShoppingCartItems.Select(p => p.Name));
                // Calculate the ItemPrice, ProductQuantity, TotalPrice, and TotalQuantity
                
              
                sale.TotalPrice = TotalPrice;
                sale.TotalQuantity = TotalQuantity;
                // Save the Sale object to the database
                using (var dbContext = new POSContext())
                {
                    dbContext.Sales.Add(sale);
                    dbContext.SaveChanges();
                }

                // Clear the ShoppingCartItems, TotalPrice, and TotalQuantity properties
                ShoppingCartItems.Clear();
                TotalPrice = 0;
                TotalQuantity = 0;

                // Show a message box or perform any other necessary logic
                MessageBox.Show("Sale completed!");
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"An error occurred while saving the sale. Error: {ex.InnerException?.Message}");
            }
        }


        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
