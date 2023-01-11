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
            StatusSelect.ItemsSource = Enum.GetValues(typeof( BO.eOrderStatus));
            order = bl.iOrder.Read(orderId);
            orderItemsview.ItemsSource = order.Items;
            IDTXT.Text = order.OrderID.ToString();
            NameTXT.Text = order.CustomerName;
            OrderDateTXT.Text = order.Order_Date.ToString();
            EmailTXT.Text = order.CustomerEmail;
            AddressTXT.Text = order.CustomerAddress;
            TotalPriceTXT.Text = order.TotalPrice.ToString();
            ShipDatetxt.Text = order.Ship_Date.ToString();
            DeliveryDateTXT.Text = order.Delivery_Date.ToString();
           StatusSelect.Text = order.Status.ToString();
        }

        public OrderWindow(IBl bl, object selectedItem)
        {
            this.bl = bl;
            this.selectedItem = selectedItem;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
            this.Close();
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
                order.Status = (BO.eOrderStatus)StatusSelect.SelectedItem;
                order.CustomerName = NameTXT.Text;
                order.CustomerEmail= EmailTXT.Text;
                order.CustomerAddress= AddressTXT.Text;
                order.Order_Date = Convert.ToDateTime(OrderDateTXT.Text);
                order.Ship_Date = Convert.ToDateTime(ShipDatetxt.Text);
                order.Delivery_Date = Convert.ToDateTime(DeliveryDateTXT.Text);
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
