using BlApi;
using BO;
using System;
using System.Windows;
using System.Windows.Controls;
namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public BO.Product product { get; set; }
        public Cart cart { get; set; }
        private IBl bl;
        public int productID { get; set; } 
        private bool ToUpdate { get; set; } = false;


        /// <summary>
        /// conctructor of this page
        /// </summary>
        /// <param name="Bl"></param>
        /// <param name="id">in case of update product</param>
        public ProductWindow(IBl Bl,int? id = null,BO.Cart cart_= null)
        {
            bl = Bl;
            cart= cart_;    
            InitializeComponent();
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            if (id != null)
            {
                productID=(int)id;
                ToUpdate = true;
                deleteBTN.Visibility = Visibility.Visible;    
                product = bl.iProduct.ProductDetails((int)id);
                NameTXT.Text = product.Name;
                PriceTXT.Text = product.Price.ToString();
               // InStockTXT.Text = product.InStock.ToString();
                categorySelector.Text = product.Category.ToString();
                AddUpdateBTN.Content = "update the product";
            }
            else
            {
                AddUpdateBTN.Content = "add the product";
                categorySelector.Text =((BO.eCategory)0).ToString();
            }
        }


        /// <summary>
        /// gets the values of the category enums
        /// </summary>
        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product.Category = (BO.eCategory)categorySelector.SelectedItem;

        }
       
        /// <summary>
        /// go back to the products list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
            this.Close();
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
        private void NameTXT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PriceTXT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InStockTXT_TextChanged(object sender, TextChangedEventArgs e)
        {

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
                this.Close();
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
            new NewOrderWindow(bl,cart).Show();
            this.Close();
        }
    }
}
