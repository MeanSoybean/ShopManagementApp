using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopManagementApp.UserControls
{
    /// <summary>
    /// Interaction logic for MasterDataUserControl.xaml
    /// </summary>
    public partial class MasterDataUserControl : UserControl
    {
        public enum MasterDataAction
        {
            AddNewCategory,
            DeleteSelectedCategory,
            AddNewProduct,
            EditSelectedProduct,
            DeleteSelectedProduct,
        }
        public MasterDataUserControl()
        {
            InitializeComponent();
        }
        public void HandleParentEvent(MasterDataAction action)
        {
            switch (action)
            {
                //TODO
                case MasterDataAction.AddNewCategory:
                    break;
                case MasterDataAction.DeleteSelectedCategory:
                    break;
                case MasterDataAction.AddNewProduct:
                    break;
                case MasterDataAction.EditSelectedProduct:
                    break;
                case MasterDataAction.DeleteSelectedProduct:
                    break;
                default:
                    break;
            }
        }

        private readonly MyShopEntities _db = new MyShopEntities();
        private void MasterDataUserControl_Initialized(object sender, EventArgs e)
        {
            ReloadInitialize();
        }

        public void ReloadInitialize()
        {
            CategoryComboBox.ItemsSource = _db.Categories.ToList();
            CategoryComboBox.SelectedIndex = 0;
        }

        class PagingRow
        {
            public int Page { get; set; }
            public int TotalPages { get; set; }
        }

        class PagingInfo
        {
            public List<PagingRow> Items { get; set; }
            public PagingInfo(int totalPages)
            {
                Items = new List<PagingRow>();
                for (int i = 1; i <= totalPages; i++)
                {
                    Items.Add(new PagingRow()
                    {
                        Page = i,
                        TotalPages = totalPages
                    });
                }
            }
        }

        private int _totalProducts;
        private int _totalPages;
        private readonly int _rowsPerPage = 5;
        private int _currentPage;
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = CategoryComboBox.SelectedItem as Category;
            _totalProducts = category.Products.Count;
            _totalPages = _totalProducts / _rowsPerPage;
            if ((_totalProducts % _rowsPerPage) != 0) _totalPages++;
            _currentPage = 1;

            var pagingInfo = new PagingInfo(_totalPages);
            PageSelectComboBox.ItemsSource = pagingInfo.Items;
            PageSelectComboBox.SelectedIndex = _currentPage - 1;
        }
        private void PageSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = CategoryComboBox.SelectedItem as Category;
            var next = PageSelectComboBox.SelectedItem as PagingRow;
            if (next == null) return;
            _currentPage = next.Page;

            ProductsDataGrid.ItemsSource = category.Products
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage);
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            var currentIndex = PageSelectComboBox.SelectedIndex;
            if (currentIndex > 0)
            {
                PageSelectComboBox.SelectedIndex = currentIndex - 1;
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            var currentIndex = PageSelectComboBox.SelectedIndex;
            if (currentIndex < PageSelectComboBox.Items.Count - 1)
            {
                PageSelectComboBox.SelectedIndex = currentIndex + 1;
            }
        }

        private void ProductsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedItems = ProductsDataGrid.SelectedItems;
            var row = selectedItems[0] as Product;
        }
    }
}
