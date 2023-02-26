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
using System.Xml.Linq;

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    public IBl bl { get; set; }
    public Cart cart { get; set; }
    public Window window { get; set; }
    private ObservableCollection<BO.OrderItem> cl { get; set; } = new();

    public CartWindow(IBl BL, Window window_, Cart cart_)
    {
        InitializeComponent();
        bl = BL;
        cart = cart_;
        window = window_;
        cl = cart?.Items == null ? new() : new(cart?.Items);
        ProductsView.ItemsSource = cl;
    }

    private void SubmitOrderBTN_Click(object sender, RoutedEventArgs e)
    {
        if (cart.TotalPrice == 0)
        {
            MessageBox.Show("There are no items in the cart", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        try
        {
            bl.iCart.SubmitOrder(cart, CustomerNameTXT.Text, CustomerEmailTXT.Text, CustomerAddressTXT.Text);
            new MainWindow().Show();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private void DeleteBTN_Click(object sender, RoutedEventArgs e)
    {
        ///
        try
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.iCart.UpdateOrderItem(cart, orderItem.ProductID, 0);
            cl.Remove(orderItem);
            MessageBox.Show($"The product {orderItem.Name} has been successfully removed from the cart", $"Delete {orderItem.Name}", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

    private void back_Click(object sender, RoutedEventArgs e)
    {
        window.Show();
        this.Hide();
    }
    private void MinusBTN_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderItem? orderItem = ((FrameworkElement)sender).DataContext as BO.OrderItem;
            if (--orderItem.Amount != 0)
            {
                int index = cl.IndexOf(orderItem);
                cl.RemoveAt(index);
                cart = bl.iCart.UpdateOrderItem(cart, orderItem.ProductID, orderItem.Amount);
                cl.Insert(index, orderItem);
            }
            else
            {
                DeleteBTN_Click(sender, e);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlusBTN_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderItem orderItem = ((FrameworkElement)sender).DataContext as BO.OrderItem;
            int index = cl.IndexOf(orderItem);
            cl.Remove(orderItem);
            cart = bl.iCart.UpdateOrderItem(cart, orderItem.ProductID, (int)++orderItem.Amount);
            cl.Insert(index, orderItem);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
