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
        public OrderWindow(IBl BL, int orderId)
        {
            InitializeComponent();
            bl = BL;
            order = bl.iOrder.Read(orderId);
            DataContext = order;
            orderItemsview.ItemsSource = order.Items;
       
            OrderDateLBL.Content = order.Order_Date.ToString();
            //ShipDateLBL.Content = order.Ship_Date.ToString();
            //DeliveryDateLBL.Content = order.Delivery_Date.ToString();
        }

        public OrderWindow(IBl bl, object selectedItem)
        {
            this.bl = bl;
            this.selectedItem = selectedItem;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
            this.Hide();
        }

        private void updateShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.iOrder.UpdateOrderShipped(order.OrderID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.iOrder.UpdateOrderDelivered(order.OrderID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void UpdateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.TotalPrice = TotalPriceTXT.Text == "" ? -1 : Convert.ToDouble(TotalPriceTXT.Text);
                order.CustomerName = NameTXT.Text;
                order.CustomerEmail= EmailTXT.Text;
                order.CustomerAddress= AddressTXT.Text;
                bl.iOrder.Update(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StatusSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
