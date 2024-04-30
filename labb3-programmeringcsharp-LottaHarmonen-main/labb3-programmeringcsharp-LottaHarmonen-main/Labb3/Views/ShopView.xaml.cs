using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {

        public ShopView()
        {
            InitializeComponent();

            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            UserManager.CurrentUserChanged += ProductManagerOnProductListChanged;
            ProductManager.CartListChanged += ProductManagerOnCartListChanged;

        }

        private void ProductManagerOnCartListChanged()
        {
            CartList.Items.Clear();

            foreach (Product product in UserManager.CurrentUser.Cart)
            {
                CartList.Items.Add(product);
            }

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

            CartList.Items.Clear();
            if (UserManager.CurrentUser != null)
            {
                foreach (Product product in UserManager.CurrentUser.Cart)
                {
                    CartList.Items.Add(product);
                }
            }
            else
            {
                return;
            }
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //anropa ProductManager.RemoveProduct(ta med product from text box);
            if (CartList.SelectedItem is Product selectedItem)
            {
                ProductManager.RemoveProduct(selectedItem);
            }
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //anropa ProductManager.AddProduct(ta med product från text box);
            if (ProdList.SelectedItem is Product selectedItem)
            {
                ProductManager.AddProduct(selectedItem);
            }
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();
        }

        private void CheckoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            UserManager.CurrentUser.Cart.Clear();
            CartList.Items.Clear();

            CheckOutPopup.IsOpen = true;

        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CheckOutPopup.IsOpen = false;
        }


        private void ProductTypeOption_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //linq för att hämta värden med den produkt typen 

            if (ProductTypeOption.SelectedItem is ProductTypes type)
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
