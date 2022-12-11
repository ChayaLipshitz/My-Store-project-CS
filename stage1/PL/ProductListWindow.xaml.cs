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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;
        
        public ProductListWindow(IBl Bl )
        {
            InitializeComponent();
            bl = Bl;
            ProductsListview.ItemsSource = bl.iProduct.ReadAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }      

        private void AddProductBTN_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl).Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();

        }

        private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            BO.eCategory category = (BO.eCategory)CategorySelector.SelectedItem;
            ProductsListview.ItemsSource = bl.iProduct.ReadByCategory(category);
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
           new ProductWindow(bl,((BO.ProductForList)ProductsListview.SelectedItem).ID).Show();
            this.Hide();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
