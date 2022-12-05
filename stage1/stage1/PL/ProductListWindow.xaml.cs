﻿using BlApi;
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
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
