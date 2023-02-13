using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>

    public partial class NewOrderWindow : Window
    {
        public IBl bl;
        public BO.Cart cart { get; set; }
        Window window;
        public NewOrderWindow(IBl BL, Window window_, Cart cart_)
        {
            InitializeComponent();
            bl = BL;
            cart = cart_;
            CatalogView.ItemsSource = bl.iProduct.ReadCatalog();
            window = window_;
        }

        /// <summary>
        /// Displays the selected product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatalogView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow(bl, this, ((BO.ProductItem)CatalogView.SelectedItem).ID, cart).Show();
            this.Hide();
        }

        /// <summary>
        /// Diplays the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartBTN_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(bl, this, cart).Show();
            this.Hide();
        }

        /// <summary>
        /// Displays the latest window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            window.Show();
        }
    }
}
