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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BO.Product product = new();
        private IBl bl;
        private bool ToUpdate { get; set; } = false;


        /// <summary>
        /// conctructor of this page
        /// </summary>
        /// <param name="Bl"></param>
        /// <param name="id">in case of update product</param>
        public ProductWindow(IBl Bl, int? id=null)
        {
            bl = Bl;
            InitializeComponent();
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            if (id != null)
            {
                ToUpdate = true;
                product = bl.iProduct.ProductDetails((int)id);
                NameTXT.Text = product.Name;
                PriceTXT.Text = product.Price.ToString();
                InStockTXT.Text = product.InStock.ToString();
                categorySelector.Text = product.Category.ToString();
                SubmitBTN.Content = "update the product";
            }
            else
            {
                SubmitBTN.Content = "add the product";
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
        /// the function takes the properties from the user and update / create the product
        /// </summary>
        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
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
        /// <summary>
        /// go back to the products list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
            this.Hide();
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
    /*
        NameTXT.BorderBrush = Brushes.Black;
                PriceTXT.BorderBrush = Brushes.Black;
                InStockTXT.BorderBrush = Brushes.Black;



                {
                    if (NameTXT.Text == "") { NameTXT.BorderBrush = Brushes.Red; }
                    product.Name = NameTXT.Text;
                    if (PriceTXT.Text == "") { PriceTXT.BorderBrush = Brushes.Red; }
                    product.Price = PriceTXT.Text == "" ? -1 : Convert.ToInt32(PriceTXT.Text);
                    if (InStockTXT.Text == "") { InStockTXT.BorderBrush = Brushes.Red; }
                    product.InStock = InStockTXT.Text == "" ? -1 : Convert.ToInt32(InStockTXT.Text);*/

}
