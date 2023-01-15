using BlApi;
using BO;
using System;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;
    public Cart cart { get; set; } 
    public MainWindow()
    {
        bl = Factory.Get();
        InitializeComponent();
        if(cart==null)
        {
            cart = new();
        }
    }

    private void AdminBTN_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow(bl).Show();
        this.Close();
    }

    private void TrackBTN_Click(object sender, RoutedEventArgs e)
    {
        int id=0;
        try
        {
            id = OrderNumTXT.Text==""? throw new Exception("please enter order id!"): Convert.ToInt32(OrderNumTXT.Text);
            BO.OrderTracking orderTracking = bl.iOrder.Tracking(id);
            new OrderTracking(bl, orderTracking).Show();
            this.Close();
        }
        catch(NotExistExceptions ex)
        {
            MessageBox.Show($"there is no order with id {id}");
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);

        }

    }

    private void NewOrderBTN_Click(object sender, RoutedEventArgs e)
    {
        new NewOrderWindow(bl,cart).Show();
        this.Close();
    }

    /// <summary>
    /// button to the product window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

}
