using BlApi;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;

    public MainWindow()
    {
        bl = Factory.Get();
        InitializeComponent();
    }

    private void AdminBTN_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow(bl).Show();
        this.Close();
    }

    /// <summary>
    /// button to the product window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

}
