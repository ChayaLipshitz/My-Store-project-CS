using BlApi;
using BO;
using System;
using System.Windows;
using System.Windows.Controls;
namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    public BO.Product product { get; set; } = new();
    public Cart cart { get; set; }
    private IBl bl;
    public int productID { get; set; }
    private bool ToUpdate { get; set; } = false;
    public Window window { get; set; }

    /// <summary>
    /// conctructor of this page
    /// </summary>
    /// <param name="Bl"></param>
    /// <param name="id">in case of update product</param>
    public ProductWindow(IBl Bl,Window window_, int? id = null, BO.Cart cart_ = null)
    {
        bl = Bl;
        cart = cart_;
        window = window_;
        InitializeComponent();
        categorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        if (id != null)
        {
            if (cart == null)
            {
                productID = (int)id;
                ToUpdate = true;
                deleteBTN.Visibility = Visibility.Visible;
                product = bl.iProduct.ProductDetails((int)id);
                DataContext = product;
                AddUpdateBTN.Content = "Update the product";
            }
            else
            {
                productID = (int)id;
                product = bl.iProduct.ProductDetails((int)id);
                DataContext = product;
                AddToCartBTN.Visibility = Visibility.Visible;
                AddUpdateBTN.Visibility = Visibility.Hidden;
            }

        }
        else
        {
            AddUpdateBTN.Content = "Add the product";
            categorySelector.Text = ((BO.eCategory)0).ToString();
        }
    }




    /// <summary>
    /// go back to the products list window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void back_Click(object sender, RoutedEventArgs e)
    {
        window.Show();
        this.Hide();
    }
    private void deleteBTN_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (MessageBox.Show($"are you sure you want to delete {product.Name}", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                bl.iProduct.Delete(productID);
                MessageBox.Show("the product was deleted!");
                new ProductListWindow(bl).Show();
                this.Close();
            }

        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message);
        }
    }



    /// <summary>
    /// the function takes the properties from the user and update/create the product
    /// </summary>

    private void AddUpdateBTN_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            product.Name = NameTXT.Text;
            product.Price = PriceTXT.Text == "" ? -1 : Convert.ToDouble(PriceTXT.Text);
            product.InStock = InStockTXT.Text == "" ? -1 : Convert.ToInt32(InStockTXT.Text);
            product.Category = (BO.eCategory)categorySelector.SelectedItem;
            if (ToUpdate)
                bl.iProduct.Update(product);
            else
                bl.iProduct.Add(product);
            new ProductListWindow(bl).Show();
            this.Hide();
        }
        catch (Dal.DO.DuplicateIdExceptions err)
        {
            MessageBox.Show(err.Message);
        }
        catch (BO.PropertyInValidException err)
        {
            MessageBox.Show(err.Message);

        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message);
        }


    }


    private void AddToCartBTN_Click_1(object sender, RoutedEventArgs e)
    {
        bl.iCart.addOrderItem(cart, productID);
        new NewOrderWindow(bl, cart).Show();
        this.Hide();
    }

    /// <summary>
    /// gets the values of the category enums
    /// </summary>
    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        //  product.Category = (BO.eCategory)categorySelector.SelectedItem;


    }
    private void NameTXT_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PriceTXT_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void InStockTXT_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
