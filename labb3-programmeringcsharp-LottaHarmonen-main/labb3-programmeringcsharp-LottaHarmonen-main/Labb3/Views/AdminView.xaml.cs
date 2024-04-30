using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public MainWindowContext MainWindowContext { get; set; }

        public AdminView()
        {
            InitializeComponent();
            MainWindowContext = new MainWindowContext();

            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;

            ProductManager.ProductListChanged += ProductManagerOnProductListChanged;

            DataContext = MainWindowContext;

        }

        private void ProductManagerOnProductListChanged()
        {
            ProdList.Items.Clear();

            foreach (Product product in ProductManager.Products)
            {
                ProdList.Items.Add(product);
            }
        }

        private void UserManager_CurrentUserChanged()
        {
            ProdList.Items.Clear();

            foreach (Product product in ProductManager.Products)
            {
                ProdList.Items.Add(product);
            }
        }

        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ProdList.SelectedItem is Product selectedItem)
            {
                MainWindowContext.ProductName = selectedItem.Name;
                MainWindowContext.ProductPrice = selectedItem.Price;

                Product selectedProduct = (Product)ProdList.SelectedItem;
                ProductTypeOption.SelectedItem = selectedProduct.ProductTypes;
            }
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string name = MainWindowContext.ProductName;
            double price = MainWindowContext.ProductPrice;
            Product newProduct;
          

            if (ProductTypeOption.SelectedItem is ProductTypes.Dairy)
            {
                newProduct = new Dairy(name, price, "../ImageFolder/NoImage.jpg");
                ProductManager.AddProduct(newProduct);
            }
            else if (ProductTypeOption.SelectedItem is ProductTypes.Meat)
            {
                newProduct = new Meat(name, price, "../ImageFolder/NoImage.jpg");
                ProductManager.AddProduct(newProduct);
            }
            else if (ProductTypeOption.SelectedItem is ProductTypes.Fruit)
            {
                newProduct = new Fruit(name, price, "../ImageFolder/NoImage.jpg");
                ProductManager.AddProduct(newProduct);
            }
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProdList.SelectedItem is Product selectedProduct)
            {
                ProductManager.RemoveProduct(selectedProduct);
            }

        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();
        }

        private void ProductTypeSorting_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductTypeSorting.SelectedItem is ProductTypes type)
            {
                var selectedProductType = ProductManager.Products.Where(p => p.ProductTypes == type);

                ProdList.Items.Clear();
                foreach (Product p in selectedProductType)
                {
                    ProdList.Items.Add(p);
                }
            }
        }
    }
}
