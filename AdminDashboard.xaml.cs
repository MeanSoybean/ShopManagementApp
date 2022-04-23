using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Fluent;
using ShopManagementApp.UserControls;

namespace ShopManagementApp
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : RibbonWindow
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var screens = new ObservableCollection<TabItem>()
            {
                new TabItem() { Content = new MasterDataUserControl() },
                new TabItem() { Content = new SalesUserControl() },
                new TabItem() { Content = new ReportUserControl() },
            };
            Tabs.ItemsSource=screens;
        }

        private void ExitMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ExcelImportButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void AddCategoryButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCategoryButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void EditProductButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteProductButton_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
