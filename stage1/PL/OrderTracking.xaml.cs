using BlApi;
using BO;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BO.OrderTracking orderTracking;
        int orderID;
        public IBl bl { get; set; }
        public OrderTracking(IBl BL, BO.OrderTracking orderTracking)
        {
            InitializeComponent();
            bl = BL;
            orderID = orderTracking.ID;
            orderIDLBL.Content = orderID;
            OrderStatusLBL.Content = orderTracking.Status.ToString();
            DateStatusView.ItemsSource = new ObservableCollection<(DateTime?,eOrderStatus?)>(orderTracking?.dateAndStatus);
            
        }

        private void OrderDetailsBTN_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow(bl, orderID).Show();
            this.Close();
        }

        private void DateStatusView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
