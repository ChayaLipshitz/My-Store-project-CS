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
    public CartWindow(IBl BL, Window window_, Cart cart_)
    {
        InitializeComponent();
        bl = BL;
        cart = cart_;
        window = window_;
        ProductsView.ItemsSource = cart_.Items;
    }

    private void SubmitOrderBTN_Click(object sender, RoutedEventArgs e)
    {
        bl.iCart.SubmitOrder(cart, CustomerNameTXT.Text, CustomerEmailTXT.Text, CustomerAddressTXT.Text);
        new MainWindow().Show();
        this.Close();
    }



    private void DeleteBTN_Click(object sender, RoutedEventArgs e)
    {
        ///
        try
        {
            BO.OrderItem obj = ((FrameworkElement)sender).DataContext as BO.OrderItem;
            int id = obj.ProductID;
            cart = bl.iCart.UpdateOrderItem(cart, id, 0);
            MessageBox.Show("The product has been successfully removed from the cart");

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    private void back_Click(object sender, RoutedEventArgs e)
    {
        window.Show();
        this.Hide();
    }

    private void ProductsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void MinusBTN_Click(object sender, RoutedEventArgs e)
    {
        BO.OrderItem orderItem = ((FrameworkElement)sender).DataContext as BO.OrderItem;
        int index = cart.Items.FindIndex(oi => oi.ProductID == orderItem.ProductID);
        if (index == -1) throw new NotExistExceptions();
        orderItem = cart.Items[index];
        if (orderItem.Amount <= 1) cart.Items.Remove(orderItem);
        else cart.Items[index].Amount--;
        new CartWindow(bl,window, cart).Show();
        this.Close();
    }

    private void PlusBTN_Click(object sender, RoutedEventArgs e)
    {
        BO.OrderItem? orderItem = ((FrameworkElement?)sender)?.DataContext as BO.OrderItem;
        int index = cart.Items.FindIndex(oi => oi.ProductID == orderItem.ProductID);
        if (index == -1) throw new NotExistExceptions();
        orderItem = cart.Items[index];
        BO.Product product = bl.iProduct.ProductDetails(orderItem.ProductID);
        if (orderItem.Amount < product.InStock)
        {
            cart.Items[index].Amount++;
            new CartWindow(bl, window, cart).Show();
            this.Close();
        }
    }


    //  <GridViewColumn Header = "Amount" Width="80" x:Name="newAmountColounm">
    //<GridViewColumn.CellTemplate >
    //    <DataTemplate>
    //        <TextBox Text = "{Binding Path=Amount, Mode=TwoWay}" x:Name="newAmountTxt" Width="80"/>
    //    </DataTemplate>
    //</GridViewColumn.CellTemplate>
    //</GridViewColumn>


    //<GridViewColumn Header = "update new amount" Width="150">
    //    <GridViewColumn.CellTemplate >
    //        <DataTemplate>
    //            <Button x:Name="updateNewAmount" Click="updateAmount_Button_Click" Width="150" ></Button>
    //        </DataTemplate>
    //    </GridViewColumn.CellTemplate>
    //</GridViewColumn>
}
