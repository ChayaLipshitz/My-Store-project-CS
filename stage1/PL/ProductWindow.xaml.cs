using BlApi;
using BO;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
namespace PL {

    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public BO.Product product { get; set; } = new();
        public Cart? cart { get; set; }

        public Tuple<Product, bool,bool,string?> ToData { get; set; } 
        private IBl bl;
        private bool ToUpdate { get; set; } = false;
        private bool ToAdd { get; set; } = false;
        public Window window { get; set; }
        //////////////----------dependency object!!-----------////////////////////
        public static readonly DependencyProperty IsManagerProperty = DependencyProperty.Register(
        "IsManager", typeof(bool),
        typeof(ProductWindow)
        );

        public bool IsManager
        {
            get => (bool)GetValue(IsManagerProperty);
            set => SetValue(IsManagerProperty, value);
        }

        /// <summary>
        /// conctructor of this page
        /// </summary>
        /// <param name="Bl"></param>
        /// <param name="id">in case of update product</param>
        public ProductWindow(IBl Bl, Window window_, int? id = null, BO.Cart? cart_ = null)
        {
            if (Bl == null) MessageBox.Show("there is a problem!!!!!");
            InitializeComponent();
            bl = Bl;
            cart = cart_ ?? null;
            window = window_;
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            if (id != null)
            {
                product = bl.iProduct.ProductDetails((int)id);
                if (cart == null)
                {
                    ToUpdate = true;
                }                
            }
            else
            {
                ToAdd = true;
                product.Category = (BO.eCategory)0;
            }
            IsManager = (cart == null);
            ToData = new(product, IsManager, !IsManager, ToAdd ? "Add the product" : ToUpdate ? "Update the product" : "");
            DataContext = ToData;
        }


        /// <summary>
        /// go back to the products list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            window.Show();
            this.Hide();
        }

        /// <summary>
        /// Deleting the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Are you sure you want to delete {product.Name}?", $"Deleting {product.Name}", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    bl.iProduct.Delete(product.ID);
                    MessageBox.Show("The product has been successfully deleted!", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    new ProductListWindow(bl).Show();
                    this.Hide();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// the function takes the properties from the user and update/create the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToStore_Or_UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ToUpdate)
                {
                    bl.iProduct.Update(product);
                    MessageBox.Show("Product updated successfully!", "Update profuct", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    bl.iProduct.Add(product);
                    MessageBox.Show("Product added successfully!", "Add profuct", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                new ProductListWindow(bl).Show();
                this.Hide();
            }
            catch (Dal.DO.DuplicateIdExceptions err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.PropertyInValidException err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// The function add the product to the cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCartBTN_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.iCart.addOrderItem(cart, product.ID);
                new NewOrderWindow(bl, this, cart).Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

    }    
}