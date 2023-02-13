using BlApi;
using BO;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
        Window window;
        public IBl bl { get; set; }
        public OrderTracking(IBl BL,Window window_, int orderTrackingID)
        {
            InitializeComponent();
            Activated += Render;
            bl = BL;
            window = window_;
            orderTracking = bl.iOrder.Tracking(orderTrackingID);
            orderIDLBL.Content = orderTracking.ID;
            OrderStatusLBL.Content = orderTracking.Status.ToString();
            DateStatusView.ItemsSource = new ObservableCollection<Tuple<DateTime?,eOrderStatus?>>(orderTracking?.dateAndStatus);
            
        }

        /// <summary>
        /// The function (event) refreshes the status list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Render(object? sender, EventArgs e)
        {
            orderTracking = bl.iOrder.Tracking(orderTracking.ID);
            orderIDLBL.Content = orderTracking.ID;
            OrderStatusLBL.Content = orderTracking.Status.ToString();
            DateStatusView.ItemsSource = new ObservableCollection<Tuple<DateTime?, eOrderStatus?>>(orderTracking?.dateAndStatus);
        }

        /// <summary>
        /// Displaying a specific order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderDetailsBTN_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow(bl,this, orderTracking.ID).Show();
            this.Hide();
        }

        /// <summary>
        /// Displaying the latest window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            window.Show();
            this.Hide();
        }
    }
}