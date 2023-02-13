using BlApi;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;
        /// <summary>
        /// ctor of this page
        /// </summary>
        /// <param name="Bl"></param>        
        public ProductListWindow(IBl Bl)
        {
            InitializeComponent();
            Activated += Render;
            bl = Bl;
            ProductsListview.ItemsSource = bl.iProduct.ReadAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            OrdersListview.ItemsSource = bl.iOrder.ReadAll();
        }

        /// <summary>
        /// The function (event) refreshes the lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Render(object? sender, EventArgs e)
        {
            ProductsListview.ItemsSource = bl.iProduct.ReadAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            OrdersListview.ItemsSource = bl.iOrder.ReadAll();
        }

        /// <summary>
        /// The function calls the window of adding product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductBTN_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl, this).Show();
            this.Hide();
        }

        /// <summary> 
        /// The function presents only the producs which in the requested category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BO.eCategory category = (BO.eCategory)CategorySelector.SelectedItem;
                var tmp = bl.iProduct.ReadByCategory(category);
                ProductsListview.ItemsSource = tmp;
            }catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Send the user to the screen of adding page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow(bl, this, ((BO.ProductForList)ProductsListview.SelectedItem).ID).Show();
            this.Hide();
        }

        /// <summary>
        /// Go back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

        /// <summary>
        /// The fiunction call all the products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NonFilter_Click(object sender, RoutedEventArgs e)
        {
            ProductsListview.ItemsSource = bl.iProduct.ReadAll();
        }

        /// <summary>
        /// Displaying the products' list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productsListBTN_Click(object sender, RoutedEventArgs e)
        {
            CategorySelector.Visibility = Visibility.Visible;
            CategoryLBL.Visibility = Visibility.Visible;
            OrdersListview.Visibility = Visibility.Collapsed;
            ProductsListview.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Disappearring the products' list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ordersListBTN_Click(object sender, RoutedEventArgs e)
        {
            CategoryLBL.Visibility = Visibility.Collapsed;
            CategorySelector.Visibility = Visibility.Collapsed;
            OrdersListview.Visibility = Visibility.Visible;
            ProductsListview.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Call to a selected Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdesListview_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            new OrderWindow(bl, this, ((BO.OrderForList)OrdersListview.SelectedItem).ID).Show();
            this.Hide();
        }        
    }
}
