using BlApi;
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
    public partial class OrderWindow : Window
    {
        private IBl bl;
        private object selectedItem;

        public OrderWindow(IBl BL,int orderId)
        {
            InitializeComponent();
            bl= BL;
            orderItemsview.ItemsSource = (System.Collections.IEnumerable)bl.iOrder.Read(orderId);
        }

        public OrderWindow(IBl bl, object selectedItem)
        {
            this.bl = bl;
            this.selectedItem = selectedItem;
        }
    }
}
