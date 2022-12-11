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
        public ProductWindow(IBl Bl)
        {
            bl = Bl;
            InitializeComponent();
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));


        }

       

        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           product.Category = (BO.eCategory)categorySelector.SelectedItem;

        }

        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                product.Name = NameTXT.Text;
                //if (product.Name == "")
                //{
                //    NameTXT.BorderBrush = new Brush('AliceBlue');
                //}
                product.Price = Convert.ToInt32(PriceTXT.Text);
                product.InStock = Convert.ToInt32(InStockTXT.Text);
                product.Category = (BO.eCategory)categorySelector.SelectedItem;
                bl.iProduct.Add(product);
                new ProductListWindow(bl).Show();
                this.Hide();
            }
            catch (Dal.DO.DuplicateIdExceptions err)
            {
                throw new BO.DataError(err);
            }
            catch (BO.PropertyInValidException ex)
            {
                throw ex;
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

        
    }
}
