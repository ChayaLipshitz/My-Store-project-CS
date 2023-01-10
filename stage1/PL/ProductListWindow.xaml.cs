using BlApi;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;   
        /// <summary>
        /// ctor of this page
        /// </summary>
        /// <param name="Bl"></param>
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
            this.Close();
        }

        /// <summary> 
        /// the function presents only the producs which in the requested category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            BO.eCategory category = (BO.eCategory)CategorySelector.SelectedItem;
            var tmp = bl.iProduct.ReadByCategory(category);
            ProductsListview.ItemsSource = tmp;
        }

        /// <summary>
        /// send the user to the screen of adding page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
           new ProductWindow(bl,((BO.ProductForList)ProductsListview.SelectedItem).ID).Show();
            this.Close();
        }

        /// <summary>
        /// go back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NonFilter_Click(object sender, RoutedEventArgs e)
        {
            ProductsListview.ItemsSource =bl.iProduct.ReadAll();
        }
    }
}
