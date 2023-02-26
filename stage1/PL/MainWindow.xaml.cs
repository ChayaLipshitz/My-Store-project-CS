using BlApi;
using BO;
using System;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction log for MainWindow.xaml
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

    /// <summary>
    /// Calls the manager's screen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminBTN_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow(bl).Show();
        this.Hide();
    }

    /// <summary>
    /// Calls the trucking at orders screen with the order id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrackBTN_Click(object sender, RoutedEventArgs e)
    {
        int id=0;
        try
        {
            id = OrderNumTXT.Text==""? throw new Exception("please enter order id!"): Convert.ToInt32(OrderNumTXT.Text);
            BO.OrderTracking orderTracking = bl.iOrder.Tracking(id);
            new OrderTracking(bl,this, orderTracking.ID).Show();
            this.Hide();
        }
        catch(NotExistExceptions ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

    /// <summary>
    /// Calls the client screen - opens a new order.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NewOrderBTN_Click(object sender, RoutedEventArgs e)
    {
        new NewOrderWindow(bl,this,cart).Show();
        this.Hide();
    }

    private void Simulator_Click(object sender, RoutedEventArgs e)
    {
        new SimulatorWindow(bl).Show();
    }
}