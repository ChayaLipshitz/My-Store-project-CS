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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    /// 
    public partial class OrderWindow : Window
    {
        private IBl bl;
        private object selectedItem;
        private BO.Order order;
        Window window;
        /// <summary>
        /// Window of a specific order
        /// </summary>
        /// <param name="BL"></param>
        /// <param name="window_"></param>
        /// <param name="orderId"></param>
        public OrderWindow(IBl BL, Window window_, int orderId)
        {
            InitializeComponent();
            bl = BL;
            window = window_;
            order = bl.iOrder.Read(orderId);
            DataContext = order;
            orderItemsview.ItemsSource = order.Items;
        }

        //is it Unnecessary?////////////////////////////////////////
        public OrderWindow(IBl bl, Window window_, object selectedItem)
        {
            InitializeComponent();
            this.bl = bl;
            this.window = window_;
            this.selectedItem = selectedItem;
        }

        /// <summary>
        /// The function call the latest window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            window.Show();
            this.Hide();
        }

        /// <summary>
        /// The Function updating the order status - to shipping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl.iOrder.UpdateOrderShipped(order.OrderID);
                DataContext = order;
                orderItemsview.ItemsSource = order.Items;
                MessageBox.Show("Shipping updated successfully!", "Update Shipping",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// The Function is updating the order status - to shipping 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl.iOrder.UpdateOrderDelivered(order.OrderID);
                DataContext = order;
                orderItemsview.ItemsSource = order.Items;
                MessageBox.Show("Delivering updated successfully!", "Update Delivering", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        /// <summary>
        /// The function updating the orders details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.TotalPrice = TotalPriceTXT.Text == "" ? -1 : Convert.ToDouble(TotalPriceTXT.Text);
                order.CustomerName = NameTXT.Text;
                order.CustomerEmail = EmailTXT.Text;
                order.CustomerAddress = AddressTXT.Text;
                bl.iOrder.Update(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
